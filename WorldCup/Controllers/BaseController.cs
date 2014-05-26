using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using WorldCup.Common;
using WorldCup.Models.Identity;

namespace WorldCup.Controllers
{
    public abstract class ControllerBase : AsyncController
    {

        private ApplicationUserManager _userManager;

        protected string UserSavedSuccessfullyKey = "UserSavedSuccessfully";

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                       (_userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());
            }
        }

        private ApplicationDbContext _context;

        protected ApplicationDbContext Context
        {
            get { return _context ?? (_context = HttpContext.GetOwinContext().Get<ApplicationDbContext>()); }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var lastUpdateTime = Session["LastUpdateTime"] as string;
            if(string.IsNullOrEmpty(lastUpdateTime) && Context.Parameters.Any(p=>p.Name == PredefinedParameters.LastUpdateTime))
            {
                if(filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.IsNewSession)
                {
                    lastUpdateTime = Context.Parameters.First(p => p.Name == PredefinedParameters.LastUpdateTime).Value;
                    Session["LastUpdateTime"] = lastUpdateTime;
                }
            }

            ViewBag.LastUpdateTime = lastUpdateTime;

            base.OnActionExecuting(filterContext);
        }

    }
}