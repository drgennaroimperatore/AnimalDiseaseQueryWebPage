using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public JsonResult InsertUser(HDB context, ILRIUser user)
        {
            ILRIUser oldUser = null;
           
            try
            {
                if (context.Users.Count() > 0)
                {
                    int usersCount = context.Users.Where(x => x.UUID.Equals(user.UUID)).Count();


                    if (usersCount == 0)
                    {
                        context.Users.Add(user);
                        context.SaveChanges();

                    }
                    else
                    {
                        oldUser = context.Users.Where(x => x.UUID.Equals(user.UUID)).First();
                    }
                }
                else
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch(Exception e)
            {
               Console.WriteLine(e.Message);
                return Json(new InsertionOutcome { outcome = e.Message, ID = "200" });
            }
            if(oldUser!= null)
                return Json(new InsertionOutcome { outcome = "Found", ID = oldUser.ID.ToString() });

            return Json(new InsertionOutcome { outcome = "Success", ID = user.ID.ToString() });
        }

        public JsonResult InsertFarmer(HDB context, Farmer farmer)
        {
            try
            {
                if(context.Farmers.Count()>0)
                {
                    if(context.Farmers.Select(f => f.ID).Contains(farmer.ID))
                        return Json(new InsertionOutcome { outcome = "Success", ID = farmer.ID.ToString() });

                }
                if (farmer.ID < 0)
                    farmer.ID = 0;
         
                context.Farmers.Add(farmer);
                context.SaveChanges();
            }
            catch (DbUpdateException dbu)
            {
                var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

                try
                {
                    foreach (var result in dbu.Entries)
                    {
                        builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                        Debug.WriteLine(result.Entity.GetType().ToString());
                    }
                }
                catch (Exception e)
                {
                    builder.Append("Error parsing DbUpdateException: " + e.ToString());
                }

                Debug.WriteLine(builder.ToString());
                    }

         

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(@"Entity of type ""{0}"" in state ""{1}"" 
                   has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine(@"- Property: ""{0}"", Error: ""{1}""",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }




            }
            return Json(new InsertionOutcome { outcome = "Success", ID = farmer.ID.ToString() });
        }

     
        public JsonResult InsertHerd(HDB context, Herd herd)
        {
            if(context.Herds.Count()>0)
            {
                if(context.Herds.Select(h => h.ID).Contains(herd.ID))
                {
                    return Json(new InsertionOutcome { outcome = "Found", ID = herd.ID.ToString() });
                }
            }
            if (herd.ID < 0)
                herd.ID = 0;
            context.Herds.Add(herd);
            context.SaveChanges();

            return Json( new InsertionOutcome { outcome ="Success", ID = herd.ID.ToString() });
        }

        public JsonResult InsertHerdVisit(HDB context, HerdVisit herdVisit)
        {
            if(context.HerdVisits.Count()>0)
            {
                if(context.HerdVisits.Select(hv=>hv.ID).Contains(herdVisit.ID))
                    return Json(new InsertionOutcome { outcome = "Fount", ID = herdVisit.ID.ToString() });
            }

            if (herdVisit.ID < 0)
                herdVisit.ID = 0;
            context.HerdVisits.Add(herdVisit);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = herdVisit.ID.ToString() });
        }
        #region HEALTH EVENTS
        public JsonResult InsertHealthEvent (HDB context, HealthEvent healthEvent)
        {
            if(context.HealthEvents.Count()>0)
            {
                if(context.HealthEvents.Select(he=>he.ID).Contains(healthEvent.ID))
                    return Json(new InsertionOutcome { outcome = "Found", ID = healthEvent.ID.ToString() });
            }
            if (healthEvent.ID < 0)
                healthEvent.ID = 0;
            context.HealthEvents.Add(healthEvent);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = healthEvent.ID.ToString() });
        }

        public JsonResult InsertDiseaseForHealthEvent(HDB context, DiseasesForHealthEvent dhe)
        {
            if(context.DiseasesForHealthEvents.Count()>0)
            {
                if(context.DiseasesForHealthEvents.Select(d=> d.ID).Contains(dhe.ID))
                    return Json(new InsertionOutcome { outcome = "Found", ID = dhe.ID.ToString() });
            }

            if (dhe.ID < 0)
                dhe.ID = 0;
            context.DiseasesForHealthEvents.Add(dhe);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = dhe.ID.ToString() });
        }

        public JsonResult InsertSignForHealthEvent(HDB context, SignsForHealthEvent she)
        {
            if(context.SignsForHealthEvents.Count()>0)
            {
                if(context.SignsForHealthEvents.Select(s=> s.ID).Contains(she.ID))
                  {
                    return Json(new InsertionOutcome { outcome = "Found", ID = she.ID.ToString() });
                }
            }
            if (she.ID < 0)
                she.ID = 0;
            context.SignsForHealthEvents.Add(she);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = she.ID.ToString() });
        }

        #endregion

        #region PRODUCTIVITY EVENT
        [AllowAnonymous]
        public JsonResult InsertProductivityEvent(HDB context, ProductivityEvent productivityEvent)
        {
            if(context.ProductivityEvents.Count()>0)
            {
                if(context.ProductivityEvents.Select(p => p.ID).Contains(productivityEvent.ID))
                    return Json(new InsertionOutcome { outcome = "Found", ID = productivityEvent.ID.ToString() });
            }

            if (productivityEvent.ID < 0)
                productivityEvent.ID = 0;
            context.ProductivityEvents.Add(productivityEvent);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = productivityEvent.ID.ToString()});
        }

        public JsonResult InsertMilkForProductivityEvent(HDB context, MilkForProductivityEvent mpe)
        {
            if(context.MilkForProductivityEvents.Count()>0)
            {
                if(context.MilkForProductivityEvents.Select(m => m.ID).Contains(mpe.ID))
                    return Json(new InsertionOutcome { outcome = "Found", ID = mpe.ID.ToString() });
            }

            if (mpe.ID < 0)
                mpe.ID = 0;
            context.MilkForProductivityEvents.Add(mpe);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = mpe.ID.ToString() });
        }
        [AllowAnonymous]
        public JsonResult InsertBirthsForProductivityEvent(HDB context, BirthsForProductivityEvent bpe)
        {
            if(context.BirthsForProductivityEvents.Count()>0)
            {
                if(context.BirthsForProductivityEvents.Select(b => b.ID).Contains(bpe.ID))
                    return Json(new InsertionOutcome { outcome = "Found", ID = bpe.ID.ToString() });
            }

            if (bpe.ID < 0)
                bpe.ID = 0;
            context.BirthsForProductivityEvents.Add(bpe);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = bpe.ID.ToString() });
        }

        #endregion

        #region DYNAMIC EVENTS

        public JsonResult InsertDynamicEvent(HDB context, DynamicEvent dynamicEvent)
        {
            if(context.DynamicEvents.Count()>0)
            {
                if(context.DynamicEvents.Select(d =>d.ID).Contains(dynamicEvent.ID))
                    return Json(new InsertionOutcome { outcome = "Found", ID = dynamicEvent.ID.ToString() });
            }

            if (dynamicEvent.ID < 0)
                dynamicEvent.ID = 0;
            context.DynamicEvents.Add(dynamicEvent);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = dynamicEvent.ID.ToString() });
        }

        public JsonResult InsertAnimalMovementForDynamicEvent(HDB context, AnimalMovementsForDynamicEvent amde)
        {
            if(context.AnimalMovementsForDynamicEvents.Count()>0)
            {
                if(context.AnimalMovementsForDynamicEvents.Select(a=> a.ID ).Contains(amde.ID))
                    return Json(new InsertionOutcome { outcome = "Found", ID = amde.ID.ToString() });
            }

            if (amde.ID < 0)
                amde.ID = 0;
            context.AnimalMovementsForDynamicEvents.Add(amde);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success",ID= amde.ID.ToString()});
        }

        public JsonResult InsertDeathForDynamicEvent(HDB context, DeathsForDynamicEvent dde)
        {
            if(context.DeathsForDynamicEvents.Count()>0)
            {
                if(context.DeathsForDynamicEvents.Select(dd => dd.ID).Contains(dde.ID))
                    return Json(new InsertionOutcome { outcome = "Success", ID = dde.ID.ToString() });
            }

            if (dde.ID < 0)
                dde.ID = 0;

            context.DeathsForDynamicEvents.Add(dde);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome = "Success", ID = dde.ID.ToString() });
        }

        #endregion

    }
}