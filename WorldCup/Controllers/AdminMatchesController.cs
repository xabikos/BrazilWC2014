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
    public class AdminMatchesController : ControllerBase
    {
        public ViewResult Index()
        {
            return View(Context.Matches.OrderBy(m => m.Date));
        }

        public async Task<ActionResult> Add()
        {
            ViewBag.Teams = await Context.Teams.ToListAsync();
            return View(new Match());
        }

        [HttpPost]
        public async Task<ActionResult> Add(Match model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Teams = await Context.Teams.ToListAsync();
                return View(model);
            }

            Context.Matches.Add(model);
            await Context.SaveChangesAsync();

            TempData[UserSavedSuccessfullyKey] = "You successfully added the match";

            return RedirectToAction("Add");
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == default (int))
                return View(new Match());

            var match = await Context.Matches.FirstAsync(m => m.Id == id);

            PrepareViewBag(id, match);

            return View(match);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Match model)
        {
            var match = await Context.Matches.FirstAsync(m => m.Id == model.Id);

            //Check if we try to edit a match that has already some calculations
            if (Context.MatchPoints.Any(mp => mp.MatchId == match.Id))
            {
                ModelState.AddModelError("MatchClosedForModification","There are calculation based on this match so the modification is not allowed");
            }

            if (!ModelState.IsValid)
            {
                PrepareViewBag(model.Id, match);
                model.HomeTeam = match.HomeTeam;
                model.AwayTeam = match.AwayTeam;
                return View(model);
            }

            match.State = model.State;
            match.Date = model.Date;
            match.HomeTeamHalfTimeGoals = model.HomeTeamHalfTimeGoals;
            match.AwayTeamHalfTimeGoals = model.AwayTeamHalfTimeGoals;
            match.HomeTeamFullTimeGoals = model.HomeTeamFullTimeGoals;
            match.AwayTeamFullTimeGoals = model.AwayTeamFullTimeGoals;
            match.YellowCards = model.YellowCards;
            match.RedCards = model.RedCards;
            match.Result = model.Result;

            await Context.SaveChangesAsync();

            TempData[UserSavedSuccessfullyKey] = "You successfully edit the match";

            return RedirectToAction("Edit", new {id = model.Id});
        }

        private void PrepareViewBag(int id, Match match)
        {
            var matchesIds = Context.Matches.OrderBy(m => m.Date)
                .Select(m => m.Id)
                .ToList()
                .FindSandwichedItem(m => m == id)
                .ToList();
            ViewBag.PreviousMatchId = matchesIds[0] != 0 ? matchesIds[0] : id;
            ViewBag.NextMatchId = matchesIds[1] != 0 ? matchesIds[1] : id;
        }

    }
}