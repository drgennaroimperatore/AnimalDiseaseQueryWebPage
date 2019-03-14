using AnimalDiseaseQueryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace AnimalDiseaseQueryWebApp.Controllers
{
    public class EditDBController : Controller
    {
        // GET: EditDB
        public ActionResult Index(ADDB context, EditDBViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (TempData["Errors"] != null)
                    ModelState.AddModelError("Animal Name", (string)TempData["Errors"]);
            }

            try
            {



                model.animals = context.Animals.ToList();
                model.diseases = context.Diseases.ToList();
                model.priorsDiseases = context.PriorsDiseases.ToList();
                
            
                model.signs = context.Signs.ToList();
                model.likelihoods = context.Likelihoods.ToList();
              

                foreach (Animal n in model.animals)
                {
                    model.animalNames.Add(n.Name);

                }


            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null)
                      ? ex.InnerException.Message
                      : "";

                return PartialView("_ErrorDB", new ErrorDBViewModel(innerMessage));
            }

            return View(model);


        }
        #region Animals Table
        [HttpPost]
        public ActionResult InsertNewAnimal(ADDB context, string name, EditDBViewModel model)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                TempData["Errors"] = "Missing Fields";
                return RedirectToAction("Index", "EditDB", model);
            }

            name = name.ToUpper();
            //insert male

            if (context.Animals.Where(m=> m.Name == name).Count() >0)
            {
                TempData["Errors"] = "This Name Already Exists";
                return RedirectToAction("Index", "EditDB", model);
            }

           
                

                //baby
                Animal a = new Animal();
                a.Name = name;
                context.Animals.Add(a);
                a.Sex = "M";
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


            return RedirectToAction("Index", "EditDB", model);




        }

        [HttpPost]
        public ActionResult EditAnimal(ADDB context, int idToEdit)
        {
            return RedirectToAction("Index", "EditDB");
        }


        [HttpPost]
        public ActionResult RemoveAnimal(ADDB context, string name)
        {
            var animalsToRemove = context.Animals.Where(m => m.Name == name);

            //remove foreign key references before removing the animal 
            foreach (var a in animalsToRemove)
                foreach (var pd in context.PriorsDiseases.Where(m => m.AnimalID == a.Id))
                    context.PriorsDiseases.Remove(pd);

            context.Animals.RemoveRange(animalsToRemove);
            context.SaveChanges();

            return RedirectToAction("Index", "EditDB");
        }


        #endregion

        #region Signs Table

        public ActionResult InsertNewSign(ADDB context, Sign sign)
        {
            if(String.IsNullOrWhiteSpace(sign.Name))
            {
                TempData["Errors"] = "Sign Name is missing";
            }

            sign.Name = sign.Name.ToUpper();

            

            if(context.Signs.Where(s=> s.Name == sign.Name).Count()>0)
            {
                TempData["Errors"] = "Sign "+ sign.Name +" already exists!";
                return RedirectToAction("Index");
            }

            //TO DO ADDITIONAL CHECKS 
            switch (sign.Type_of_Value)
            {
                case SignTypes.NUMERICAL:
                    break;

                case SignTypes.OBSERVATIONAL:

                    break;
            }

            context.Signs.Add(sign);
            context.SaveChanges();


            TempData["Errors"] = null;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveSign(ADDB context, int id)
        {
            Sign signToRemove = context.Signs.Find(id);

            foreach (Likelihood l in context.Likelihoods.Where(m => m.SignId == id).ToList())
                context.Likelihoods.Remove(l);

            context.Signs.Remove(signToRemove);

            context.SaveChanges();



            return RedirectToAction("Index");
        }

        #endregion

        #region Disease Table
        public ActionResult InsertNewDisease(ADDB context, Disease disease )
        {
            if (String.IsNullOrWhiteSpace(disease.Name))
            {
                TempData["Errors"] = "Missing Fields";
                return RedirectToAction("Index");
            }

            disease.Name = disease.Name.ToUpper();

            if (context.Diseases.Where(d=> d.Name == disease.Name).Count() > 0)
            {
                TempData["Errors"] = "Disease " + disease.Name + " already exists!";
                return RedirectToAction("Index");
            }
            
                context.Diseases.Add(disease);
               
                context.SaveChanges();


            TempData["Errors"] = null;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveDisease(ADDB context, int id)
        {
            Disease diseaseToRemove = context.Diseases.Find(id);

            foreach (var pd in context.PriorsDiseases.Where(m => m.DiseaseID == id))
                context.PriorsDiseases.Remove(pd);

            foreach (var d in context.Likelihoods.Where(m => m.DiseaseId == id))
                context.Likelihoods.Remove(d);

            
            context.Diseases.Remove(diseaseToRemove);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult InsertDiseasePrior(ADDB context, int diseaseID, int animalID, string probability)
        {
            PriorsDiseases priorsDiseases = new PriorsDiseases();
            priorsDiseases.AnimalID = animalID;
            priorsDiseases.DiseaseID = diseaseID;
            priorsDiseases.Probability = probability;
            context.PriorsDiseases.Add(priorsDiseases);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RemoveDiseasePrior(ADDB context, int id)
        {
           PriorsDiseases priorToRemove = context.PriorsDiseases.Find(id);
            context.PriorsDiseases.Remove(priorToRemove);
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

        public ActionResult RemoveLikelihood(ADDB context, int id)
        {
            Likelihood likelihoodToRemove = context.Likelihoods.Find(id);
            context.Likelihoods.Remove(likelihoodToRemove);

            return RedirectToAction("Index");
        }


        #endregion
    }
}