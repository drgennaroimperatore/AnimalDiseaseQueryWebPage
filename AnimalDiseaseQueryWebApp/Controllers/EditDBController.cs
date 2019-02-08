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
            return View(model);
        }
                #region Animals Table
        [HttpPost]
        public ActionResult InsertNewAnimal(ADDB context, Animal animal, EditDBViewModel model)
        {
            if (animal.Name == null)
            {
                TempData["Errors"] = "Missing Fields";
            }
            
            else
            {

                TempData["Errors"] = null;
                context.Animals.Add(animal);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "EditDB",model);



        }

        [HttpPost]
        public ActionResult EditAnimal(ADDB context, int idToEdit)
        {
            return RedirectToAction("Index", "EditDB");
        }


        [HttpPost]
        public ActionResult RemoveAnimal(ADDB context, int idToRemove)
        {
            Animal animalToRemove = context.Animals.Find(idToRemove);
            context.Animals.Remove(animalToRemove);
            context.SaveChanges();

            return RedirectToAction("Index", "EditDB");
        }


        #endregion

        #region Signs Table

        #endregion

        #region Disease Table
        public ActionResult InsertNewDisease (ADDB context, Disease disease)
        {
            if (disease.Name == null)
            {
                TempData["Errors"] = "Missing Fields";
            }
            else
            {
                context.Diseases.Add(disease);
                context.SaveChanges();
            }


            return RedirectToAction("Index");
        }
        #endregion

    }
}