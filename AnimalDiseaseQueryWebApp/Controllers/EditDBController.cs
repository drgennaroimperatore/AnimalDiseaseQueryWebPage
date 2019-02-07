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
        public ActionResult Index(ADDB context)
        {
            EditDBViewModel model = new EditDBViewModel();
            model.animals = context.Animals.ToList();
            return View(model);
        }
        #region Animals Table
        [HttpPost]
        public ActionResult InsertNewAnimal(ADDB context, Animal animal)
        {
            //if(animal.Name==null)
            //    ModelState.AddModelError("Name", "The Animal Name is required");

            if (ModelState.IsValid)
            {

                context.Animals.Add(animal);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "EditDB");


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

    }
}