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

            var longRunningResults = await Context.LongRunningResults.FirstOrDefaultAsync();

            if(longRunningResults == null)
            {
                return View("Index",
                    model: "Select the correct teams for long running results and then try to update the rankings");
            }

            // Calculate only for users that have done long running predictions
            foreach(var user in UserManager.AllUsers.Where(u => u.LongRunningPrediction != null).ToList())
            {
                if(user.LongRunningPoints == null)
                {
                    user.LongRunningPoints = new LongRunningPoints();
                }

                // Second stage teams
                var correctSecondStageUserTeams =
                    longRunningResults.SecondStageTeamsIds.Intersect(user.LongRunningPrediction.SecondStageTeamsIds)
                        .Where(el => !string.IsNullOrEmpty(el));
                user.LongRunningPoints.SecondStagePoints = correctSecondStageUserTeams.Count()*
                                                           systemParameters.Round16TeamsFactor;

                // Quarter final stage teams
                var correctQuarterFinalUserTeams =
                    longRunningResults.QuarterFinalTeamsIds.Intersect(user.LongRunningPrediction.QuarterFinalTeamsIds)
                        .Where(el => !string.IsNullOrEmpty(el));
                user.LongRunningPoints.QuarterFinalPoints = correctQuarterFinalUserTeams.Count()*
                                                            systemParameters.QuarterFinalTeamsFactor;

                // Semi final stage teams
                var correctSemiFinalUserTeams =
                    longRunningResults.SemiFinalTeamsIds.Intersect(user.LongRunningPrediction.SemiFinalTeamsIds)
                        .Where(el => !string.IsNullOrEmpty(el));
                user.LongRunningPoints.SemiFinalPoints = correctSemiFinalUserTeams.Count()*
                                                         systemParameters.SemiFinalTeamsFactor;

                // Small final stage teams
                var correctSmallFinalUserTeams =
                    longRunningResults.SmallFinalTeamsIds.Intersect(user.LongRunningPrediction.SmallFinalTeamsIds)
                        .Where(el => !string.IsNullOrEmpty(el));
                user.LongRunningPoints.SmallFinalPoints = correctSmallFinalUserTeams.Count()*
                                                          systemParameters.SmallFinalTeamsFactor;

                // Small final stage teams
                var correctFinalUserTeams =
                    longRunningResults.FinalTeamsIds.Intersect(user.LongRunningPrediction.FinalTeamsIds)
                        .Where(el => !string.IsNullOrEmpty(el));
                user.LongRunningPoints.FinalPoints = correctFinalUserTeams.Count()*
                                                     systemParameters.SmallFinalTeamsFactor;

                // Winner of tournament points
                if(!string.IsNullOrEmpty(longRunningResults.WinnerTeamId) &&
                   longRunningResults.WinnerTeamId == user.LongRunningPrediction.WinnerTeamId)
                {
                    user.LongRunningPoints.WinnerPoints = systemParameters.WinnerTeamFactor;
                }
            }

            await Context.SaveChangesAsync();

            return View("Index", model: "You successfully update long running rankings");
        }

        #region calculate match points

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

        #endregion

    }
}