using System.Linq;
using System.Web.Mvc;

namespace WorldCup.Controllers
{
    [Authorize]
    public class MatchesController : ControllerBase
    {

        public ActionResult Index()
        {
            return View(Context.Matches.OrderBy(m => m.Date));
        }
    }
}