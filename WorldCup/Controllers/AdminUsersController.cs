using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(UserManager.AllUsers.OrderBy(u => u.UserName));
        }

        [HttpPost]
        public async Task<ActionResult> Activate(string userId)
        {
            try
            {
                var user = await UserManager.ChangeUserStatus(userId, true);
                return Json(string.Format("Successfully activate user {0}", user.FirstName + " " + user.LastName),
                    JsonRequestBehavior.AllowGet);
            }
            catch (InvalidOperationException ex)
            {
                return new HttpNotFoundResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Deactivate(string userId)
        {
            try
            {
                var user = await UserManager.ChangeUserStatus(userId, false);
                return Json(string.Format("Successfully deactivate user {0}", user.FirstName + " " + user.LastName),
                    JsonRequestBehavior.AllowGet);
            }
            catch (InvalidOperationException ex)
            {
                return new HttpNotFoundResult(ex.Message);
            }
        }

    }
}