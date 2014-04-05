using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldCup.Models;
using Microsoft.AspNet.Identity.Owin;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        public ActionResult Index()
        {
            var usersContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            return View(usersContext.Users.OrderBy(u => u.UserName));
        }

    }
}