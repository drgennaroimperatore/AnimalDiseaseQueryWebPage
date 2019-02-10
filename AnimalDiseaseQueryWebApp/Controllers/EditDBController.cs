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