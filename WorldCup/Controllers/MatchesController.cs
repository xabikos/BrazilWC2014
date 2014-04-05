using System.Web.Mvc;

namespace WorldCup.Controllers
{
    [Authorize]
    public class MatchesController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}