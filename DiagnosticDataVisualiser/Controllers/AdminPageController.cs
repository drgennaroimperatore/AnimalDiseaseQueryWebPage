using DiagnosticDataVisualiser.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiagnosticDataVisualiser.Controllers
{
    public class AdminPageController : Controller
    {
        // GET: AdminPage
        public ActionResult Index(AdminPageViewModel model)
        {
            model.currentUser = User.Identity.Name;
            var userManager =HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var u = userManager.Users;
            

            return View(model);
        }
    }
}