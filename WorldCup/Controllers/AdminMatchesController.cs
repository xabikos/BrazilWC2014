using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

        public async Task<ViewResult> Edit(int id)
        {
            if (id == default (int))
                return View(new Match());

            var match = await Context.Matches.SingleOrDefaultAsync(m => m.Id == id);

            return View(match);
        }

    }
}