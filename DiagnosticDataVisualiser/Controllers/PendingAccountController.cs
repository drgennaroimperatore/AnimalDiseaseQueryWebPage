using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiagnosticDataVisualiser.Controllers
{
    public class PendingAccountController : Controller
    {
        // GET: PendingAccount
        public ActionResult Index()
        {
            return View();
        }
    }
}