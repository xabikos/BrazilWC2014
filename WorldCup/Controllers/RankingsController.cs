using System.Web.Mvc;

namespace WorldCup.Controllers
{
    [Authorize]
    public class RankingsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}