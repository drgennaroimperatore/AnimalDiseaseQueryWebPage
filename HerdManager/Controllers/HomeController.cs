using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HerdManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HDB context)
        {
            if(context.BodyConditions.Count()==0)
            {
                //populate the body condition reference table if it is empty
                //context.BodyCondition.AddRange(BodyCondition.populate());
            }
            context.SaveChanges();
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

        public JsonResult InsertUser(HDB context, User user)
        {
            User oldUser = null;
           
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
          
                if(context.Farmers.Count()>0)
                {
                    if(context.Farmers.Select(f => f.ID).Contains(farmer.ID))
                        return Json(new InsertionOutcome { outcome = "Success", ID = farmer.ID.ToString() });

                }
                if (farmer.ID < 0)
                    farmer.ID = 0;
         
                context.Farmers.Add(farmer);
                context.SaveChanges();
    
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
            //string dateString = new DateTime(herdVisit.HerdVisitDate);
            //IFormatProvider culture = new CultureInfo("en-GB", true);
           // herdVisit.HerdVisitDate = dateString; // DateTime.ParseExact(dateString, "yyyy-MM-dd", culture);
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
                    SignsForHealthEvent oldShe = context.SignsForHealthEvents.Where(b => b.ID == she.ID).FirstOrDefault();
                    if (she.HasBeenUpdated(oldShe))
                    {
                        oldShe.numberOfAffectedBabies = she.numberOfAffectedBabies;
                        oldShe.numberOfAffectedOld = she.numberOfAffectedOld;
                        oldShe.numberOfAffectedYoung = she.numberOfAffectedYoung;
                        context.SaveChanges();
                        return Json(new InsertionOutcome { outcome = "Updated", ID = she.ID.ToString() });
                    }

                    else
                        return Json(new InsertionOutcome { outcome = "Found", ID = she.ID.ToString() });
                }
            }
            if (she.ID < 0)
                she.ID = 0;
            context.SignsForHealthEvents.Add(she);
            context.SaveChanges();

            

            return Json(new InsertionOutcome { outcome = "Success", ID = she.ID.ToString() });
        }

        [AllowAnonymous]
        public JsonResult InsertBodyConditionForHealthEvent(HDB context, BodyConditionForHealthEvent bodyConditionForHealthEvent)
        {
            if(context.BodyConditionForHealthEvents.Count()>0)
            {
                if (context.BodyConditionForHealthEvents.Select(s => s.ID).Contains(bodyConditionForHealthEvent.ID))
                {
                    BodyConditionForHealthEvent oldBhce = context.BodyConditionForHealthEvents.Find(bodyConditionForHealthEvent);
 
                    if(bodyConditionForHealthEvent.HasBeenUpdated(oldBhce))
                    {
                        oldBhce = bodyConditionForHealthEvent;
                        return Json(new InsertionOutcome { outcome = "Updated", ID = bodyConditionForHealthEvent.ID.ToString() });
                    }
                    else
                        return Json(new InsertionOutcome { outcome = "Found", ID = bodyConditionForHealthEvent.ID.ToString() });

                    }
            }
            if(bodyConditionForHealthEvent.ID<0)
               bodyConditionForHealthEvent.ID = 0;
        
            
            context.BodyConditionForHealthEvents.Add(bodyConditionForHealthEvent);
            context.SaveChanges();
       

            return Json(new InsertionOutcome { outcome = "Success", ID = bodyConditionForHealthEvent.ID.ToString() });
        }

        public JsonResult InsertHealthInterventionForHealthEvent(HDB context, HealthInterventionForHealthEvent healthInterventionForHealthEvent)
        {
            if (context.HealthInterventionForHealthEvents.Count() > 0)
            {
                if (context.HealthInterventionForHealthEvents.Select(s => s.ID).Contains(healthInterventionForHealthEvent.ID))
                {
                    HealthInterventionForHealthEvent oldHihe = context.HealthInterventionForHealthEvents.Find(healthInterventionForHealthEvent.ID);
                    if(oldHihe.HasBeenUpdated(healthInterventionForHealthEvent))
                    {
                        oldHihe.numberOfAffectedBabies = healthInterventionForHealthEvent.numberOfAffectedBabies;
                        oldHihe.numberOfAffectedYoung = healthInterventionForHealthEvent.numberOfAffectedYoung;
                        oldHihe.numberOfAffectedAdult = healthInterventionForHealthEvent.numberOfAffectedAdult;
                        context.SaveChanges();
                        return Json(new InsertionOutcome { outcome = "Updated", ID = healthInterventionForHealthEvent.ID.ToString() });
                    }
                
                    return Json(new InsertionOutcome { outcome = "Found", ID = healthInterventionForHealthEvent.ID.ToString() });
                }
            }
            if (healthInterventionForHealthEvent.ID < 0)
                healthInterventionForHealthEvent.ID = 0;


            context.HealthInterventionForHealthEvents.Add(healthInterventionForHealthEvent);
            context.SaveChanges();

            return Json(new InsertionOutcome { outcome ="Success", ID = healthInterventionForHealthEvent.ID.ToString()});
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
                if (context.MilkForProductivityEvents.Select(m => m.ID).Contains(mpe.ID))
                {
                    MilkForProductivityEvent oldMpe = context.MilkForProductivityEvents.Find(mpe.ID);
                    if(mpe.HasBeenUpdate(oldMpe))
                    {
                        oldMpe.numberOfLactatingAnimals = mpe.numberOfLactatingAnimals;
                        oldMpe.litresOfMilkPerDay = mpe.litresOfMilkPerDay;
                        return Json(new InsertionOutcome { outcome = "Updated", ID = mpe.ID.ToString() });
                    }
                    else
                        return Json(new InsertionOutcome { outcome = "Found", ID = mpe.ID.ToString() });
                }
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
                if (context.BirthsForProductivityEvents.Select(b => b.ID).Contains(bpe.ID))
                {
                    BirthsForProductivityEvent oldBpe = context.BirthsForProductivityEvents.Find(bpe.ID);
                    if(oldBpe.HasBeenUpdated(bpe))
                    {
                        oldBpe.nOfGestatingAnimals = bpe.nOfGestatingAnimals;
                        oldBpe.nOfBirths = bpe.nOfBirths;
                        context.SaveChanges();
                        return Json(new InsertionOutcome { outcome = "Updated", ID = bpe.ID.ToString() });
                    }
                    else
                        return Json(new InsertionOutcome { outcome = "Found", ID = bpe.ID.ToString() });
                }
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