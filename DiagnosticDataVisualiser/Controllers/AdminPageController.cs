using AspNet.Identity.MySQL;
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
        public ActionResult Index(Eddie context, AdminPageViewModel model)
        {
           

            return View(model);
        }
    }
}