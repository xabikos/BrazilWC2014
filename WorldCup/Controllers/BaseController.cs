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
        const string Navigationbrand = "NavigationBrand";
        const string Lastupdatetime = "LastUpdateTime";

        protected string UserSavedSuccessfullyKey = "UserSavedSuccessfully";

        protected ControllerBase()
        {
            
        }

        protected ControllerBase(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                       (_userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext _context;

        protected ApplicationDbContext Context
        {
            get { return _context ?? (_context = HttpContext.GetOwinContext().Get<ApplicationDbContext>()); }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            var navigationBrand = Session[Navigationbrand] as string;
            
            var lastUpdateTime = Session[Lastupdatetime] as string;

            if(filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.IsNewSession)
            {
                // Check if navigation brand is filled and fill it
                if(string.IsNullOrEmpty(navigationBrand) &&
                   Context.Parameters.Any(p => p.Name == PredefinedParameters.NavigationBrandText))
                {
                    navigationBrand =
                        Context.Parameters.First(p => p.Name == PredefinedParameters.NavigationBrandText).Value;
                    Session[Navigationbrand] = navigationBrand;
                }
                // Check if last update time is filled and fill it
                if(string.IsNullOrEmpty(lastUpdateTime) &&
                   Context.Parameters.Any(p => p.Name == PredefinedParameters.LastUpdateTime))
                {
                    lastUpdateTime =
                        Context.Parameters.First(p => p.Name == PredefinedParameters.LastUpdateTime).Value;
                    Session[Lastupdatetime] = lastUpdateTime;
                }
            }

            ViewBag.NavigationBrand = navigationBrand;
            ViewBag.LastUpdateTime = lastUpdateTime;

            base.OnActionExecuting(filterContext);
        }

    }
}