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
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // match prediction is null the first time
            var matchPrediction = user.MatchPredictions.SingleOrDefault(mp => mp.MatchId == id) ?? new MatchPrediction();
            var match = await Context.Matches.SingleAsync(m => m.Id == id);
            matchPrediction.Match = match;
            matchPrediction.MatchId = match.Id;
            return View(matchPrediction);
        }

        [HttpPost]
        public async Task<ActionResult> MatchPrediction(MatchPrediction model)
        {
            if(!ModelState.IsValid)
            {
                var match = await Context.Matches.SingleAsync(m => m.Id == model.MatchId);
                model.Match = match;
                return View(model);
            }
            
            var applicationUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // the first time a prediction is done
            if(model.MatchPredictionId == default(int))
            {
                applicationUser.MatchPredictions.Add(model);
            }
            // user updates a prediction
            else
            {
                var userPrediction = applicationUser.MatchPredictions.Single(mp => mp.MatchPredictionId == model.MatchPredictionId);
                userPrediction.HomeTeamHalfTimeGoals = model.HomeTeamHalfTimeGoals;
                userPrediction.AwayTeamHalfTimeGoals = model.AwayTeamHalfTimeGoals;
                userPrediction.HomeTeamFullTimeGoals = model.HomeTeamFullTimeGoals;
                userPrediction.AwayTeamFullTimeGoals = model.AwayTeamFullTimeGoals;
                userPrediction.YellowCards = model.YellowCards;
                userPrediction.RedCards = model.RedCards;
                userPrediction.Result = model.Result;
            }
            
            await Context.SaveChangesAsync();

            return RedirectToAction("MatchPrediction", new {id = model.MatchId});
        }

    }
}