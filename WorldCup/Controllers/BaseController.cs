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
        
    }
}