using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldCup.Common;

namespace WorldCup.Controllers
{
    [Authorize]
    public class ReportsController : ControllerBase
    {
        // GET: Reports
        public async Task<ActionResult> UserPredictions()
        {

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.LogoSource = Context.Parameters.Single(p => p.Name == PredefinedParameters.ApplicationLogo).Value;
            ViewBag.ApplicationName = Context.Parameters.Single(p => p.Name == PredefinedParameters.ApplicationLogoText).Value;

            return new PdfActionResult(user);
        }

    }
}