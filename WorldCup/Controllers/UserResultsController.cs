using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    [Authorize]
    public class UserResultsController : ControllerBase
    {
        // GET: UserResults
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var matches = user.MatchPredictions.Where(mp => mp.Match.Date < DateTime.UtcNow)
                .OrderByDescending(mp => mp.Match.Date).ToList()
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
                        });

            return View(matches);
        }

    }
}