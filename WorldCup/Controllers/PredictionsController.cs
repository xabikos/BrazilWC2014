using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;
using WorldCup.Attributes;
using WorldCup.Common.Entities;
using WorldCup.Extensions;
using WorldCup.Models.Predictions;

namespace WorldCup.Controllers
{
    [Authorize]
    public class PredictionsController : ControllerBase
    {
        private const string SuccesMatchPredictionSaved = "You successfully saved your predictions";
        
        // The date and time of the first match in utc
        private readonly DateTime _firstMatchDate = new DateTime(2014, 6, 12, 20, 0, 0);

        // GET: Predictions
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.UserPredictions = user.MatchPredictions.Select(mp => mp.MatchId).ToList();

            // Return only the matches that are valid for predictions or are finalized
            return View(Context.Matches.Where(m => m.State != MatchState.Created).OrderBy(m => m.Date));
        }

        /// <summary>
        /// Based on the match date returns either a view to enter predictions 
        /// or a view that contains an overview of the user prediction if exists  
        /// </summary>
        public async Task<ViewResult> MatchPrediction(int id)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // match prediction is null the first time
            var match = await Context.Matches.SingleAsync(m => m.Id == id && m.State != MatchState.Created);
            var matchPrediction = user.MatchPredictions.SingleOrDefault(mp => mp.MatchId == id) ?? new MatchPrediction();

            PrepareViewBag(match);

            // In case the match has started then return the info view
            if (DateTime.UtcNow > match.Date)
            {
                var model = GetPredictionInfoModel(matchPrediction, match);

                return View("MatchPredictionInfo", model);
            }

            matchPrediction.Match = match;
            matchPrediction.MatchId = match.Id;

            return View(matchPrediction);
        }

        [HttpPost]
        [UserConfirmedFilter]
        public async Task<ActionResult> MatchPrediction(MatchPrediction model)
        {
            var match = await Context.Matches.SingleAsync(m => m.Id == model.MatchId);

            // Check if the match prediction still applies
            if (DateTime.UtcNow > match.Date || match.State != MatchState.VisibleForPredictions)
            {
                return View("TimeOverpassed");
            }

            if(!match.IsGroupStage() && model.Result == MatchResult.Draw)
            {
                ModelState.AddModelError("InvalidResult", "You are not allowed to select Draw as result for a non Group match");
            }

            if(!ModelState.IsValid)
            {
                PrepareViewBag(match);
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

            TempData[UserSavedSuccessfullyKey] = SuccesMatchPredictionSaved;

            return RedirectToAction("MatchPrediction", new {id = model.MatchId});
        }

        public async Task<ActionResult> LongRunningPredictions()
        {
            ViewBag.Teams = Context.Teams.OrderBy(t=>t.Name);
            
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            
            // In case the tournament has started return the info view that doesn't allow edit the predictions
            return DateTime.UtcNow > _firstMatchDate
                ? View("LongRunningPredictionsInfo", GetLongRunningPredictionsInfoModel(user))
                : View(user.LongRunningPrediction ?? new LongRunningPrediction());
        }

        [HttpPost]
        [UserConfirmedFilter]
        public async Task<ActionResult> LongRunningPredictions(LongRunningPrediction model)
        {
            // Check if the long running prediction still applies
            if (DateTime.UtcNow > _firstMatchDate)
            {
                return View("TimeOverpassed");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Teams = Context.Teams.OrderBy(t => t.Name);
                return View(model);
            }

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // first time a long running prediction is done
            if (user.LongRunningPrediction == null)
            {
                user.LongRunningPrediction = model;
            }
            // user updates the long running prediction
            else
            {
                user.LongRunningPrediction.SecondStageTeamsIds = model.SecondStageTeamsIds;
                user.LongRunningPrediction.QuarterFinalTeamsIds = model.QuarterFinalTeamsIds;
                user.LongRunningPrediction.SemiFinalTeamsIds = model.SemiFinalTeamsIds;
                user.LongRunningPrediction.SmallFinalTeamsIds = model.SmallFinalTeamsIds;
                user.LongRunningPrediction.FinalTeamsIds = model.FinalTeamsIds;
                user.LongRunningPrediction.WinnerTeamId = model.WinnerTeamId;
            }

            await Context.SaveChangesAsync();

            TempData[UserSavedSuccessfullyKey] = SuccesMatchPredictionSaved;

            return RedirectToAction("LongRunningPredictions");
        }

        public JsonResult LongRunningStatistics()
        {
            var confirmedUsersCount = UserManager.AllUsers.Count(u => u.EmailConfirmed);
            if(confirmedUsersCount != 0)
            {

                var secondStageStatistics = new Dictionary<string, object>
                {
                    {"stage", "Round of 16"}
                };
                Context.SecondStageStatistics.ForEach(
                    stat => secondStageStatistics.Add(stat.Code.ToLowerInvariant(), stat.Count*100/confirmedUsersCount));

                var quarterFinalStatistics = new Dictionary<string, object>
                {
                    {"stage", "Quarter-finals"}
                };
                Context.QuarterFinalStatistics.ForEach(
                    stat => quarterFinalStatistics.Add(stat.Code.ToLowerInvariant(), stat.Count*100/confirmedUsersCount));

                var semiFinalStatistics = new Dictionary<string, object>
                {
                    {"stage", "Semi-finals"}
                };
                Context.SemiFinalStatistics.ForEach(
                    stat => semiFinalStatistics.Add(stat.Code.ToLowerInvariant(), stat.Count*100/confirmedUsersCount));

                var smallFinalStatistics = new Dictionary<string, object>
                {
                    {"stage", "3rd place match"}
                };
                Context.SmallFinalStatistics.ForEach(
                    stat => smallFinalStatistics.Add(stat.Code.ToLowerInvariant(), stat.Count*100/confirmedUsersCount));

                var finalStatistics = new Dictionary<string, object>
                {
                    {"stage", "Final"}
                };
                Context.FinalStatistics.ForEach(
                    stat => finalStatistics.Add(stat.Code.ToLowerInvariant(), stat.Count*100/confirmedUsersCount));

                var winnerStatistics = new Dictionary<string, object>
                {
                    {"stage", "Winner"}
                };
                Context.WinnerStatistics.ForEach(
                    stat => winnerStatistics.Add(stat.Code.ToLowerInvariant(), stat.Count*100/confirmedUsersCount));

                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data =
                        new List<Dictionary<string, object>>
                        {
                            secondStageStatistics,
                            quarterFinalStatistics,
                            semiFinalStatistics,
                            smallFinalStatistics,
                            finalStatistics,
                            winnerStatistics
                        }
                };
            }
            // no confirmed user yet
            return null;
        }

        private void PrepareViewBag(Match match)
        {
            var matchesIds = Context.Matches.OrderBy(m => m.Date)
                .Where(m=>m.State != MatchState.Created)
                .Select(m => m.Id)
                .ToList()
                .FindSandwichedItem(m => m == match.Id)
                .ToList();
            ViewBag.PreviousMatchId = matchesIds[0] != 0 ? matchesIds[0] : match.Id;
            ViewBag.NextMatchId = matchesIds[1] != 0 ? matchesIds[1] : match.Id;
        }

        /// <summary>
        /// Creates and returns a model for match prediction overview when the match has started
        /// and it's not longer available for predictions
        /// </summary>
        /// <remarks>
        /// We have to pass as arguments both prediction and match because if the user has not done any prediction
        /// we might still want to show the match results
        /// </remarks>
        private PredictionInfoModel GetPredictionInfoModel(MatchPrediction matchPrediction, Match match)
        {
            var model = new PredictionInfoModel();
            // Check if the user has done a prediction for the match
            if (matchPrediction.Match != null)
            {
                model.UserPredictionInfo = new UserPredictionInfo
                {
                    HalfTimeScore = string.Format("{0} - {1}",
                        matchPrediction.HomeTeamHalfTimeGoals, matchPrediction.AwayTeamHalfTimeGoals),
                    FullTimeScore = string.Format("{0} - {1}",
                        matchPrediction.HomeTeamFullTimeGoals, matchPrediction.AwayTeamFullTimeGoals),
                    Winner = matchPrediction.Result.WinnerTeamName(match),
                    YellowCards = matchPrediction.YellowCards,
                    RedCards = matchPrediction.RedCards
                };
            }

            model.HomeTeamName = match.HomeTeam.Name;
            model.AwayTeamName = match.AwayTeam.Name;
            model.FinalResultsUpdated = match.State == MatchState.Finalized;
            model.HalfTimeScoreResult = string.Format("{0} - {1}", match.HomeTeamHalfTimeGoals,
                match.AwayTeamHalfTimeGoals);
            model.FullTimeScoreResult = string.Format("{0} - {1}", match.HomeTeamFullTimeGoals,
                match.AwayTeamFullTimeGoals);
            model.WinnerResult = match.Result.WinnerTeamName(match);
            model.YellowCardsResult = match.YellowCards;
            model.RedCardsResult = match.RedCards;

            var currentUserId = User.Identity.GetUserId();

            model.UsersPredictions =
                UserManager.ConfirmedUsers.Where(u => u.Id != currentUserId).SelectMany(
                    u => u.MatchPredictions.Where(mp => mp.MatchId == match.Id).Select(mp => new UserPredictionInfo
                    {
                        UserId = u.Id,
                        UserName = u.FirstName + " " + u.LastName,
                        HalfTimeScore = mp.HomeTeamHalfTimeGoals + " - " + mp.AwayTeamHalfTimeGoals,
                        FullTimeScore = mp.HomeTeamFullTimeGoals + " - " + mp.AwayTeamFullTimeGoals,
                        Winner = mp.Result == MatchResult.Home
                            ? mp.Match.HomeTeam.Name
                            : mp.Result == MatchResult.Away ? mp.Match.AwayTeam.Name : "Draw",
                        YellowCards = mp.YellowCards,
                        RedCards = mp.RedCards
                    })).ToList();

            var usersPoints = Context.MatchPoints.Where(mp => mp.MatchId == match.Id).ToList();

            foreach (var userPrediction in model.UsersPredictions)
            {
                userPrediction.MatchPoints = usersPoints.Any(up => up.UserId == userPrediction.UserId)
                    ? usersPoints.Single(up => up.UserId == userPrediction.UserId).Points
                    : 0;
            }
            model.UsersPredictions = model.UsersPredictions.OrderByDescending(up => up.MatchPoints).ToList();

            return model;
        }

        private static LongRunningPredictionsInfoModel GetLongRunningPredictionsInfoModel(ApplicationUser user)
        {
            var result = new LongRunningPredictionsInfoModel();
            
            // In case the user has not done any long running prediction then return an empty model
            if(user.LongRunningPrediction == null)
            {
                return result;
            }

            result.Round16Teams = user.LongRunningPrediction.GetSecondStageSelectedTeamsNames;
            result.QuarterFinalTeams = user.LongRunningPrediction.GetQaurterFinalSelectedTeamsNames;
            result.SemiFinalTeams = user.LongRunningPrediction.GetSemiFinalSelectedTeamsNames;
            result.SmallFinalTeams = user.LongRunningPrediction.GetSmallFinalSelectedTeamsNames;
            result.FinalTeams = user.LongRunningPrediction.GetFinalSelectedTeamsNames;
            result.WinnerTeam = user.LongRunningPrediction.WinnerTeam != null
                ? user.LongRunningPrediction.WinnerTeam.Name
                : string.Empty;

            // If there are no any calculations yet then return the result
            if(user.LongRunningPoints == null)
            {
                return result;
            }

            result.Round16TeamsPoints = user.LongRunningPoints.SecondStagePoints;
            result.QuarterFinalPoints = user.LongRunningPoints.QuarterFinalPoints;
            result.SemiFinalPoints = user.LongRunningPoints.SemiFinalPoints;
            result.SmallFinalPoints = user.LongRunningPoints.SmallFinalPoints;
            result.FinalPoints = user.LongRunningPoints.FinalPoints;
            result.WinnerPoints = user.LongRunningPoints.WinnerPoints;

            return result;
        }

    }
}