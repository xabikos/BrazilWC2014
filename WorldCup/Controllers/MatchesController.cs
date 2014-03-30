using System.Web.Mvc;

namespace WorldCup.Controllers
{
    public class MatchesController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}