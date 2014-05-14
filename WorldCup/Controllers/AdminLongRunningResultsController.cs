using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WorldCup.Common.Entities;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminLongRunningResultsController : ControllerBase
    {
        
        public async Task<ActionResult> LongRunningResults()
        {
            ViewBag.Teams = Context.Teams.OrderBy(t => t.Name);
            ViewBag.IsLongRunningPredictionsEnabled = true;

            var longRunningPredictionsResult = await Context.LongRunningResults.FirstOrDefaultAsync();

            return View(longRunningPredictionsResult ?? new LongRunningResults());
        }

        [HttpPost]
        public async Task<ActionResult> LongRunningResults(LongRunningResults model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Teams = Context.Teams.OrderBy(t => t.Name);
                ViewBag.IsLongRunningPredictionsEnabled = true;
                return View(model);
            }

            var longRunningResults = await Context.LongRunningResults.FirstOrDefaultAsync();

            if(longRunningResults == null)
            {
                Context.LongRunningResults.Add(model);
            }
            else
            {
                longRunningResults.SecondStageTeams = model.SecondStageTeams;
                longRunningResults.QuarterFinalTeams = model.QuarterFinalTeams;
                longRunningResults.SemiFinalTeams = model.SemiFinalTeams;
                longRunningResults.SmallFinalTeams = model.SmallFinalTeams;
                longRunningResults.FinalTeams = model.FinalTeams;
                longRunningResults.WinnerTeamId = model.WinnerTeamId;
            }

            await Context.SaveChangesAsync();

            TempData[UserSavedSuccessfullyKey] = "You successfully updated the long running results";

            return RedirectToAction("LongRunningResults");
        }

    }
}