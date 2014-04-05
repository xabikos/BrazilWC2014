using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using WorldCup.Models.Identity;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private ApplicationUserManager _userManager;
        
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Index()
        {
            return View(UserManager.AllUsers.OrderBy(u => u.UserName));
        }

        [HttpPost]
        public ActionResult Confirm(string userId)
        {
            
            return Json(new { success = true });
        }

    }
}