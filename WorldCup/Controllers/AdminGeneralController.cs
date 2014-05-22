using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WorldCup.Common;
using WorldCup.Common.Entities;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminGeneralController : ControllerBase
    {
        public ActionResult RaisedMoney()
        {
            return View(new RaisedMoney());
        }

        [HttpPost]
        public async Task<ActionResult> RaisedMoney(RaisedMoney model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Trim the time as we are interested only on date
            model.Date = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day);

            if (await Context.RaisedMoney.AnyAsync(rm => rm.Date.Equals(model.Date)))
            {
                var existingEntity = await Context.RaisedMoney.FirstAsync(rm => rm.Date.Equals(model.Date));
                existingEntity.Amount = model.Amount;
            }
            else
            {
                Context.RaisedMoney.Add(model);
            }

            await Context.SaveChangesAsync();

            TempData[UserSavedSuccessfullyKey] = "You successfully add or update the raised money info";

            return RedirectToAction("RaisedMoney");
        }

        public async Task<ActionResult> SystemParameters()
        {
            return View(await Context.SystemParameters.SingleOrDefaultAsync() ?? new SystemParameters());
        }

        [HttpPost]
        public async Task<ActionResult> SystemParameters(SystemParameters model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id == default(int))
            {
                Context.SystemParameters.Add(model);
            }
            else
            {
                var preferences = await Context.SystemParameters.SingleAsync();

                preferences.GroupHalfTimeScoreFactor = model.GroupHalfTimeScoreFactor;
                preferences.GroupFullTimeScoreFactor = model.GroupFullTimeScoreFactor;
                preferences.GroupYellowCardsNumberFactor = model.GroupYellowCardsNumberFactor;
                preferences.GroupRedCardsNumberFactor = model.GroupRedCardsNumberFactor;
                preferences.GroupWinnerFactor = model.GroupWinnerFactor;

                preferences.FinalsHalfTimeScoreFactor = model.FinalsHalfTimeScoreFactor;
                preferences.FinalsFullTimeScoreFactor = model.FinalsFullTimeScoreFactor;
                preferences.FinalsYellowCardsNumberFactor = model.FinalsYellowCardsNumberFactor;
                preferences.FinalsRedCardsNumberFactor = model.FinalsRedCardsNumberFactor;
                preferences.FinalsWinnerFactor = model.FinalsWinnerFactor;

                preferences.Round16TeamsFactor = model.Round16TeamsFactor;
                preferences.QuarterFinalTeamsFactor = model.QuarterFinalTeamsFactor;
                preferences.SemiFinalTeamsFactor = model.SemiFinalTeamsFactor;
                preferences.SmallFinalTeamsFactor = model.SmallFinalTeamsFactor;
                preferences.FinalTeamsFactor = model.FinalTeamsFactor;
                preferences.WinnerTeamFactor = model.WinnerTeamFactor;
            }

            await Context.SaveChangesAsync();

            TempData[UserSavedSuccessfullyKey] = "You successfully saved the values";

            return RedirectToAction("SystemParameters");
        }

        public async Task<ActionResult> Parameters()
        {
            var savedParameters = await Context.Parameters.ToListAsync();
            var model =
                PredefinedParameters.Parameters.Select(
                    ep =>
                        new Parameter
                        {
                            Name = ep,
                            Value =
                                savedParameters.Any(sp => sp.Name == ep)
                                    ? savedParameters.First(sp => sp.Name == ep).Value
                                    : string.Empty
                        });
            return View(model.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Parameters(IEnumerable<Parameter> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var savedParameters = await Context.Parameters.ToListAsync();
            //First execution
            if (savedParameters.Count == 0)
            {
                Context.Parameters.AddRange(model);
            }
            // Update the existing values
            else
            {
                savedParameters.ForEach(sp => sp.Value = model.First(p => p.Name == sp.Name).Value);
                // Possible new parameters added after the first execution of the app
                var newParameters = model.Except(savedParameters, new ParametersComparer());
                Context.Parameters.AddRange(newParameters);
            }

            await Context.SaveChangesAsync();

            return RedirectToAction("Parameters");
        }

    }

    class ParametersComparer : IEqualityComparer<Parameter>
    {
        public bool Equals(Parameter x, Parameter y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Parameter obj)
        {
            return obj.Name.GetHashCode() + obj.Value.GetHashCode();
        }
    }

}