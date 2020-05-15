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
        #region HEALTH EVENTS
        public JsonResult InsertHealthEvent (HDB context, HealthEvent healthEvent)
        {
            context.HealthEvents.Add(healthEvent);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = healthEvent.ID.ToString() });
        }

        public JsonResult InsertDiseaseForHealthEvent(HDB context, DiseasesForHealthEvent dhe)
        {
            context.DiseasesForHealthEvents.Add(dhe);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = dhe.ID.ToString() });
        }

        public JsonResult InsertSignForHealthEvent(HDB context, SignsForHealthEvent she)
        {
            context.SignsForHealthEvents.Add(she);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = she.ID.ToString() });
        }

        #endregion

        #region PRODUCTIVITY EVENT
        [AllowAnonymous]
        public JsonResult InsertProductivityEvent(HDB context, ProductivityEvent productivityEvent)
        {
            context.ProductivityEvents.Add(productivityEvent);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = productivityEvent.ID.ToString()});
        }

        public JsonResult InsertMilkForProductivityEvent(HDB context, MilkForProductivityEvent mpe)
        {
            context.MilkForProductivityEvents.Add(mpe);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = mpe.ID.ToString() });
        }
        [AllowAnonymous]
        public JsonResult InsertBirthsForProductivityEvent(HDB context, BirthsForProductivityEvent bpe)
        {
            context.BirthsForProductivityEvents.Add(bpe);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = bpe.ID.ToString() });
        }

        #endregion

    }
}