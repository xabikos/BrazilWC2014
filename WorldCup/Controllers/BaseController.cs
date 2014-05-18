using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
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
        /*
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // http://afana.me/post/aspnet-mvc-internationalization-date-time.aspx
            // Parse TimeZoneOffset.
            ViewBag.TimeZoneOffset = TimeSpan.FromMinutes(0); // Default offset (Utc) if cookie is missing.
            var timeZoneCookie = Request.Cookies["_timeZoneOffset"];
            if (timeZoneCookie != null)
            {

                double offsetMinutes;
                if (double.TryParse(timeZoneCookie.Value, out offsetMinutes))
                {
                    // Store in ViewBag. You can use Session, TempData, or anything else.
                    ViewBag.TimeZoneOffset = TimeSpan.FromMinutes(offsetMinutes);
                }
            }

            base.OnActionExecuting(filterContext);
        }
        */
    }
}