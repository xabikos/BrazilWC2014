using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using WorldCup.Common.Entities;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminInitializationController : ControllerBase
    {
        public async Task<ActionResult> Parameters()
        {
            return View(await Context.SystemParameters.SingleOrDefaultAsync() ?? new SystemParameters());
        }

        [HttpPost]
        public async Task<ActionResult> Parameters(SystemParameters model)
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

            return RedirectToAction("Parameters");
        }

    }
}