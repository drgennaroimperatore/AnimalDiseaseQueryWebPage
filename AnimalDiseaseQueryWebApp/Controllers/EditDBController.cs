using AnimalDiseaseQueryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalDiseaseQueryWebApp.Controllers
{
    public class EditDBController : Controller
    {
        // GET: EditDB
        public ActionResult Index(ADDB context, EditDBViewModel model)
        {
            if(ModelState.IsValid)
            {
               if(TempData["Errors"]!=null)
                    ModelState.AddModelError("Animal Name","Required Field Was Not Filled");
            }
            
            model.animals = context.Animals.ToList();
            model.diseases = context.Diseases.ToList();
            model.signs = context.Signs.ToList();
            model.likelihoods = context.Likelihoods.ToList();

            foreach (Animal n in model.animals)
            {
                model.animalNames.Add(n.Name);
            }

            return View(model);

            
        }
                #region Animals Table
        [HttpPost]
        public ActionResult InsertNewAnimal(ADDB context, string name, EditDBViewModel model)
        {
            if (name == null)
            {
                TempData["Errors"] = "Missing Fields";
            }
          
            else
            {
                name = name.ToUpper();
                //insert male

                //baby
                Animal a = new Animal();
                a.Name = name;
                context.Animals.Add(a);
                a.Sex ="M";
                a.Age = "BABY";
                context.SaveChanges();

                //young
                a = new Animal();
                a.Name = name;
                context.Animals.Add(a);
                a.Sex = "M";
                a.Age = "YOUNG";
                context.SaveChanges();
                //old
                a = new Animal();
                a.Name = name;
                context.Animals.Add(a);
                a.Sex = "M";
                a.Age = "OLD";
                context.SaveChanges();

                //insert female

                //baby
                a = new Animal();
                a.Name = name;
                context.Animals.Add(a);
                a.Sex = "F";
                a.Age = "BABY";
                context.SaveChanges();

                //young
                a = new Animal();
                a.Name = name;
                context.Animals.Add(a);
                a.Sex = "F";
                a.Age = "YOUNG";
                context.SaveChanges();
                //old
                a = new Animal();
                a.Name = name;
                context.Animals.Add(a);
                a.Sex = "F";
                a.Age = "OLD";
                context.SaveChanges();


                TempData["Errors"] = null;
               
                
            }

            return RedirectToAction("Index", "EditDB",model);


        }

        [HttpPost]
        public ActionResult EditAnimal(ADDB context, int idToEdit)
        {
            return RedirectToAction("Index", "EditDB");
        }


        [HttpPost]
        public ActionResult RemoveAnimal(ADDB context, string name)
        {
            var animalsToRemove =  context.Animals.Where(m => m.Name == name);
            context.Animals.RemoveRange(animalsToRemove);
            context.SaveChanges();

            return RedirectToAction("Index", "EditDB");
        }


        #endregion

        #region Signs Table

        public ActionResult InsertNewSign(ADDB context, Sign sign)
        {
            context.Signs.Add(sign);
            context.SaveChanges();
            //TO DO ADDITIONAL CHECKS 
            switch(sign.Type_of_Value)
            {
                case SignTypes.NUMERICAL:
                    break;

                case SignTypes.OBSERVATIONAL:

                    break;
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveSign(ADDB context, int id)
        {
            return RedirectToAction("Index");
        }

        #endregion

        #region Disease Table
        public ActionResult InsertNewDisease (ADDB context, Disease disease, string Probability)
        {
        //    if (disease.Name == null)
        //    {
        //        TempData["Errors"] = "Missing Fields";
        //    }
        //    else
        //    {
        //        PriorsDiseases prior = new PriorsDiseases();
        //        prior.Probability = Probability;
        //        disease.PriorsDiseas = prior;

        //        context.Diseases.Add(disease);
        //        context.PriorsDiseases.Add(prior);
        //        context.SaveChanges();
        //    }


            return RedirectToAction("Index");
        }

        public ActionResult RemoveDisease(ADDB context, int id)
        {
            Disease diseaseToRemove = context.Diseases.Find(id);

        //    context.PriorsDiseases.Remove(diseaseToRemove.PriorsDiseas);
            context.Diseases.Remove(diseaseToRemove);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Likelihoods Tables

        public ActionResult InsertNewLikelihood(ADDB context, Likelihood likelihood)
        {
            context.Likelihoods.Add(likelihood);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RemoveLikelihood(ADDB context, Likelihood likelihood)
        {
            return RedirectToAction("Index");
        }


        #endregion
    }
}