using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WorldCup.Models.Identity;

namespace WorldCup.Attributes
{
    /// <summary>
    /// Attribute that checks if a user is confirmed and allowed to make predictions and use the application in general
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserConfirmedFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = filterContext.HttpContext.User.Identity.GetUserId();
            var userManager = filterContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (!userManager.IsEmailConfirmedAsync(userId).Result)
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(new {controller = "Home", action = "UserNotConfirmed"}));
                return;
            }

            base.OnActionExecuting(filterContext);
        }
        
    }
}