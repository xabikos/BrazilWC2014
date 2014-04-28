using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldCup.Common.Entities;
using WorldCup.Models.Predictions;

namespace WorldCup.Controllers
{
    [Authorize]
    public class PredictionsController : ControllerBase
    {
        // GET: Predictions
        public ActionResult Index()
        {
            return View(new PredictionsViewModel { Matches = Context.Matches.OrderBy(m => m.Date) });
        }

        public async Task<ViewResult> MatchPrediction(int id)
        {
            return View(new MatchPrediction {Match = await Context.Matches.SingleAsync(m => m.Id == id)});
        }

        [HttpPost]
        public async Task<ActionResult> MatchPrediction(MatchPrediction model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            var applicationUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if(model.Id == default(int))
            {
                applicationUser.MatchPredictions.Add(model);
            }

            await Context.SaveChangesAsync();

            return RedirectToAction("MatchPrediction", new {id = model.Id});
        }

    }
}