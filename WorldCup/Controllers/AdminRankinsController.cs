using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WorldCup.Common.Entities;
using WorldCup.Extensions;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminRankinsController : ControllerBase
    {
        // GET: AdminRankins
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> UpdateMatchRankings()
        {
            var systemParameters = await Context.SystemParameters.FirstOrDefaultAsync();

            if(systemParameters == null)
                return View("Index", model: "Initialize the system parameters and then update the rankings");

            foreach (var match in Context.Matches.Where(m=>m.State == MatchState.Finalized).ToList())
            {
                foreach (var user in UserManager.AllUsers.ToList())
                {
                    // The calculation has be done already
                    if (user.MatchPoints.Any(mp => mp.MatchId == match.Id)) 
                        continue;

                    var matchPoints = CalculateMatchPoints(user, match, systemParameters);
                    user.MatchPoints.Add(new MatchPoints
                    {
                        MatchId = match.Id,
                        Points = matchPoints
                    });
                }
            }

            await Context.SaveChangesAsync();
            return View("Index", model: "You successfully update all match rankings");
        }

        public async Task<ActionResult> UpdateLongRunningRankings()
        {
            var systemParameters = await Context.SystemParameters.FirstOrDefaultAsync();

            if (systemParameters == null)
                return View("Index", model: "Initialize the system parameters and then update the rankings");

            return View("Index", model: "You successfully update long running rankings");
        }

        private static int CalculateMatchPoints(ApplicationUser user, Match match, SystemParameters systemParameters)
        {
            // Find the user's prediction for this match if exists
            var userPrediction = user.MatchPredictions.FirstOrDefault(mp => mp.MatchId == match.Id);
            // If the user has not done prediction for this match return 0
            if (userPrediction == null)
            {
                return 0;
            }

            return match.IsGroupStage()
                ? CalculateGroupMatchResult(match, systemParameters, userPrediction)
                : CalculateStageMatchResult(match, systemParameters, userPrediction);
        }

        private static int CalculateGroupMatchResult(Match match, SystemParameters systemParameters,
            MatchPrediction userPrediction)
        {
            var result = 0;

            //Half time score
            if (userPrediction.HomeTeamHalfTimeGoals == match.HomeTeamHalfTimeGoals &&
                userPrediction.AwayTeamHalfTimeGoals == match.AwayTeamHalfTimeGoals)
            {
                result += systemParameters.GroupHalfTimeScoreFactor;
            }

            //Full time score
            if (userPrediction.HomeTeamFullTimeGoals == match.HomeTeamFullTimeGoals &&
                userPrediction.AwayTeamFullTimeGoals == match.AwayTeamFullTimeGoals)
            {
                result += systemParameters.GroupFullTimeScoreFactor;
            }

            // Winner
            if (userPrediction.Result == match.Result)
            {
                result += systemParameters.GroupWinnerFactor;
            }

            // YellowCards
            if (userPrediction.YellowCards == match.YellowCards)
            {
                result += systemParameters.GroupYellowCardsNumberFactor;
            }

            // RedCards
            if (userPrediction.RedCards == match.RedCards)
            {
                result += systemParameters.GroupRedCardsNumberFactor;
            }

            return result;
        }

        private static int CalculateStageMatchResult(Match match, SystemParameters systemParameters, MatchPrediction userPrediction)
        {
            var result = 0;

            //Half time score
            if (userPrediction.HomeTeamHalfTimeGoals == match.HomeTeamHalfTimeGoals &&
                userPrediction.AwayTeamHalfTimeGoals == match.AwayTeamHalfTimeGoals)
            {
                result += systemParameters.FinalsHalfTimeScoreFactor;
            }

            //Full time score
            if (userPrediction.HomeTeamFullTimeGoals == match.HomeTeamFullTimeGoals &&
                userPrediction.AwayTeamFullTimeGoals == match.AwayTeamFullTimeGoals)
            {
                result += systemParameters.FinalsFullTimeScoreFactor;
            }

            // Winner
            if (userPrediction.Result == match.Result)
            {
                result += systemParameters.FinalsWinnerFactor;
            }

            // YellowCards
            if (userPrediction.YellowCards == match.YellowCards)
            {
                result += systemParameters.FinalsYellowCardsNumberFactor;
            }

            // RedCards
            if (userPrediction.RedCards == match.RedCards)
            {
                result += systemParameters.FinalsRedCardsNumberFactor;
            }

            return result;
        }
        
    }
}