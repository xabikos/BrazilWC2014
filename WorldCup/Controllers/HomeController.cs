using System;
using System.Linq;
using System.Web.Mvc;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = new HomeViewModel {UpcomingMatches = Context.Matches.OrderBy(m => m.Date).Take(5).ToList()};
            return View(model);
        }

        public ActionResult Rules()
        {
            return View();
        }

    }
}