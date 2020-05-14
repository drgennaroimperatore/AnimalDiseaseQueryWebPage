using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HerdManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]

       public class InsertionOutcome
        {
          public  String outcome { get; set; }
          public  String ID { get; set; }

        }

        public JsonResult InsertFarmer(HDB context, Farmer farmer)
        {
            context.Farmers.Add(farmer);
            context.SaveChanges();
            return Json(new InsertionOutcome { outcome = "Success", ID = farmer.ID.ToString() });



        }
     
        public JsonResult InsertHerd(HDB context, Herd herd)
        {

            context.Herds.Add(herd);
            context.SaveChanges();

            return Json( new InsertionOutcome { outcome ="Success", ID = herd.ID.ToString() });
        }

        public JsonResult InsertHerdVisit(HDB context, HerdVisit herdVisit)
        {
            context.HerdVisits.Add(herdVisit);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = herdVisit.ID.ToString() });
        }

    }
}