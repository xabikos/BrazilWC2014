using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WorldCup.Common.Entities;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminMatchesController : ControllerBase
    {
        public ViewResult Index()
        {
            return View(Context.Matches.OrderBy(m => m.Date));
        }

        public async Task<ViewResult> Add()
        {
            ViewBag.Teams = Context.Teams.ToList();
            return View(new Match());
        }

        [HttpPost]
        public async Task<ActionResult> Add(Match match)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Teams = Context.Teams.ToList();
                return View(match);
            }

            Context.Matches.Add(match);
            await Context.SaveChangesAsync();

            return RedirectToAction("Add");
        }

        public async Task<ViewResult> Edit(int id)
        {
            ViewBag.Teams = Context.Teams.ToList();

            if (id == default (int))
                return View(new Match());

            var match = await Context.Matches.SingleOrDefaultAsync(m => m.Id == id);

            return View(match);
        }

    }
}