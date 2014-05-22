using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorldCup.Models.Reports;
using Microsoft.AspNet.Identity;

namespace WorldCup.Controllers
{
    [Authorize]
    public class ReportsController : ControllerBase
    {
        // GET: Reports
        public async Task<ActionResult> UserPredictions()
        {
            var model = new UserPredictionsReportsModel();

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return new PdfActionResult(user);
        }

    }
}