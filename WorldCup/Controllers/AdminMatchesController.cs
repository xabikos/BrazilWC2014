using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminMatchesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}