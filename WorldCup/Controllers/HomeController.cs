using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldCup.Common;
using WorldCup.Common.Entities;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    public class HomeController : ControllerBase
    {
        public async Task<ActionResult> Index()
        {
            var matches = new List<UserMatchModel>();
            var predictions = new List<MatchPrediction>();

            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                matches = user.MatchPredictions //.Where(mp => mp.Match.Date < DateTime.UtcNow) TODO uncomment this part
                    .OrderByDescending(mp => mp.Match.Date)
                    .Take(5)
                    .Select(
                        mp =>
                            new UserMatchModel
                            {
                                MatchId = mp.MatchId,
                                Date = mp.Match.Date,
                                Match = mp.Match.HomeTeam.Name + " vs " + mp.Match.AwayTeam.Name,
                                Points =
                                    user.MatchPoints.Count(mpoint => mpoint.MatchId == mp.MatchId) != 0
                                        ? user.MatchPoints.Single(mpoint => mpoint.MatchId == mp.MatchId).Points
                                        : 0
                            }).ToList();

                predictions = user.MatchPredictions.Where(mp => mp.Match.Date > DateTime.UtcNow)
                    .OrderBy(mp => mp.Match.Date)
                    .Take(2)
                    .ToList();
            }

            var savedParameters = await Context.Parameters.ToListAsync();

            var model = new HomeViewModel
            {
                Logo = savedParameters.Any(p => p.Name == PredefinedParameters.ApplicationLogo)
                    ? savedParameters.First(p => p.Name == PredefinedParameters.ApplicationLogo).Value
                    : string.Empty,
                LogoText = savedParameters.Any(p => p.Name == PredefinedParameters.ApplicationLogoText)
                    ? savedParameters.First(p => p.Name == PredefinedParameters.ApplicationLogoText).Value
                    : string.Empty,
                IntroductionText =
                    savedParameters.Any(p => p.Name == PredefinedParameters.IntroductionText)
                        ? savedParameters.First(p => p.Name == PredefinedParameters.IntroductionText).Value
                        : "Please enter an introduction text",
                UserLatestResults = matches,
                UserPredictionMatches = predictions,
                UpcomingMatches = Context.Matches.OrderBy(m => m.Date).Take(5).ToList()
            };

            return View(model);
        }

        public ActionResult Rules()
        {
            ViewBag.UnicefChampion = Context.Parameters.Single(p => p.Name == PredefinedParameters.UnicefChampion).Value;
            ViewBag.PlayingFee = Context.Parameters.Single(p => p.Name == PredefinedParameters.PlayingFee).Value;
            return View();
        }

        public ActionResult UserNotConfirmed()
        {
            ViewBag.PlayingFee = Context.Parameters.Single(p => p.Name == PredefinedParameters.PlayingFee).Value;
            return View();
        }

        public JsonResult RaisedMoney()
        {
            var latestRaisedMoney = Context.RaisedMoney.OrderByDescending(rm => rm.Date).Take(5).ToList();
            latestRaisedMoney.Reverse();

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = latestRaisedMoney.Select(rm => new { date = rm.Date.ToString("M"), amount = rm.Amount })
            };
        }

    }
}