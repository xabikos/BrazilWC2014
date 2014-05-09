﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldCup.Attributes;
using WorldCup.Common.Entities;

namespace WorldCup.Controllers
{
    [Authorize]
    public class PredictionsController : ControllerBase
    {
        // The date and time of the first match in utc
        private readonly DateTime _firstMatchDate = new DateTime(2014, 6, 12, 20, 0, 0);

        // GET: Predictions
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.UserPredictions = user.MatchPredictions.Select(mp => mp.MatchId).ToList();

            return View(Context.Matches.OrderBy(m => m.Date));
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
        [UserConfirmedFilter]
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

        public async Task<ActionResult> LongRunningPredictions()
        {
            ViewBag.Teams = Context.Teams.OrderBy(t=>t.Name);
            ViewBag.IsLongRunningPredictionsEnabled = DateTime.UtcNow < _firstMatchDate;
            
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return View(user.LongRunningPrediction ?? new LongRunningPrediction());
        }

        [HttpPost]
        [UserConfirmedFilter]
        public async Task<ActionResult> LongRunningPredictions(LongRunningPrediction model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teams = Context.Teams.OrderBy(t => t.Name);
                return View(model);
            }

            // Check if the long running prediction still applies
            if (DateTime.UtcNow > _firstMatchDate)
            {
                return View("TimeOverpassed");
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

            return RedirectToAction("LongRunningPredictions");
        }

    }
}