using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorldCup.Models.Predictions;

namespace WorldCup.Controllers
{
    public class PredictionsController : ControllerBase
    {
        // GET: Predictions
        public ActionResult Index()
        {
            return View(new PredictionsViewModel { Matches = Context.Matches.OrderBy(m => m.Date) });
        }

    }
}