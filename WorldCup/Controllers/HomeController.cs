using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldCup.Models;
using WorldCup.Models.Rankings;

namespace WorldCup.Controllers
{
    public class HomeController : ControllerBase
    {
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var matches =
                user.MatchPredictions //.Where(mp => mp.Match.Date < DateTime.UtcNow) TODO uncomment this part
                    .OrderByDescending(mp => mp.Match.Date)
                    .Take(5)
                    .Select(
                        mp =>
                            new UserMatchModel
                            {
                                Date = mp.Match.Date,
                                Match = mp.Match.HomeTeam.Name + " vs " + mp.Match.AwayTeam.Name,
                                Points =
                                    user.MatchPoints.Count(mpoint => mpoint.MatchId == mp.MatchId) != 0
                                        ? user.MatchPoints.Single(mpoint => mpoint.MatchId == mp.MatchId).Points
                                        : 0
                            }).ToList();

            var model = new HomeViewModel
            {
                UpcomingMatches = Context.Matches.OrderBy(m => m.Date).Take(5).ToList(),
                UserLatestResults = matches
            };
            return View(model);
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult UserNotConfirmed()
        {
            return View();
        }

    }
}