﻿using AnimalDiseaseQueryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

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

                if (TempData["LOG"] != null)
                {
                    model.logMessage = (string)TempData["LOG"];
                    TempData["LOG"] = null;
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
            CreateNewAnimal(context, name);

            return RedirectToAction("Index", "EditDB", model);
        }

        public void CreateNewAnimal(ADDB context, string name)
        {
            //data from the excel file needs to be trimmed

            if (String.IsNullOrWhiteSpace(name))
            {
                TempData["Errors"] = "Missing Fields";
                return;
            }

            name = name.ToUpper();
            //insert male

            if (context.Animals.Where(m => m.Name == name).Count() > 0)
            {
                TempData["Errors"] = "This Name Already Exists";
                return;
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
            //foreach (var a in animalsToRemove)
            //    foreach (var pd in context.PriorsDiseases.Where(m => m.AnimalID == a.Id))
            //        context.PriorsDiseases.Remove(pd);

            context.Animals.RemoveRange(animalsToRemove);
            context.SaveChanges();

            return RedirectToAction("Index", "EditDB");
        }

        public List<int> FindAllAnimalsIDsWithName(ADDB context, string name)
        {
            name = name.ToUpper(); // upcase just in case....
            List<int> ids = new List<int>();

            var animals = context.Animals.Where(n => n.Name == name).ToList();

            foreach(Animal a in animals)
            {
                ids.Add(a.Id);
            }

            return ids;
        }


        #endregion

        #region Signs Table

        public ActionResult InsertNewSign(ADDB context, Sign sign)
        {

            CreateSign(context, sign);

            return RedirectToAction("Index");
        }

        public void CreateSign(ADDB context, Sign sign)
        {
            if (String.IsNullOrWhiteSpace(sign.Name))
            {
                TempData["Errors"] = "Sign Name is missing";
                return;
            }

            sign.Name = sign.Name.ToUpper();



            if (context.Signs.Where(s => s.Name == sign.Name).Count() > 0)
            {
                TempData["Errors"] = "Sign " + sign.Name + " already exists!";
                return;
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
        }

        public int FindSignIDWithName(ADDB context, string signName)
        {
            signName = signName.ToUpper(); // safety
            return context.Signs.Where(m => m.Name.Equals(signName)).First().Id;
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
        public ActionResult InsertNewDisease(ADDB context, Disease disease)
        {

            CreateDisease(context, disease);

            TempData["Errors"] = null;

            return RedirectToAction("Index");
        }

        public void CreateDisease(ADDB context, Disease disease)
        {
            if (String.IsNullOrWhiteSpace(disease.Name))
            {
                TempData["Errors"] = "Missing Fields";
                return;
            }

            disease.Name = disease.Name.ToUpper();

            if (context.Diseases.Where(d => d.Name.Equals (disease.Name)).Count() > 0)
            {
                TempData["Errors"] = "Disease " + disease.Name + " already exists!";
                return;
            }

            context.Diseases.Add(disease);

            context.SaveChanges();

        }

        public int FindDiseaseIDWithName(ADDB context, string name)
        {
            return context.Diseases.Where(m => m.Name.Equals(name)).First().Id;
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
            CreateDiseasePrior(context, diseaseID, animalID, probability);
           

            return RedirectToAction("Index");
        }

        public void CreateDiseasePrior(ADDB context, int diseaseID, int animalID, string probability)
        {
            var duplicate = context.PriorsDiseases.Where(m => m.DiseaseID == diseaseID && m.AnimalID == animalID).ToList();
            if (duplicate.Count > 0)
            {
                duplicate[0].Probability = probability; // if we do this the prior gets updated rather than added again 
            }
            else
            {

                PriorsDiseases priorsDiseases = new PriorsDiseases();
                priorsDiseases.AnimalID = animalID;
                priorsDiseases.DiseaseID = diseaseID;
                priorsDiseases.Probability = probability;
                context.PriorsDiseases.Add(priorsDiseases);

            }

            context.SaveChanges();
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
            CreateLikelihood(context, likelihood);

            return RedirectToAction("Index");
        }

        public void CreateLikelihood(ADDB context, Likelihood likelihood)
        {
            //deal with duplicate entries
            var duplicates = context.Likelihoods.Where(m => m.AnimalId == likelihood.AnimalId && m.DiseaseId == likelihood.DiseaseId && m.SignId == likelihood.SignId);
            if (duplicates.Count() > 0)
                return; // do not add anything if we already have a likelihood for the same animal, disease and sign 
           

            context.Likelihoods.Add(likelihood);
            context.SaveChanges();
        }

        public ActionResult RemoveLikelihood(ADDB context, int id)
        {
            Likelihood likelihoodToRemove = context.Likelihoods.Find(id);
            context.Likelihoods.Remove(likelihoodToRemove);

            return RedirectToAction("Index");
        }


        #endregion

        #region LOAD FROM EXCEL




        public ActionResult LoadFromExcel(ADDB context)
        {
            string extension = ".xlsx";
            string filename = "data";
            string path = Server.MapPath(@"~/Files/" + filename + extension);

            try
            {
                Excel.Application app = new Excel.Application();

                Excel.Workbook WB = app.Workbooks.Open(path);

                // statement get the workbookname  
                string ExcelWorkbookname = WB.Name;

                // statement get the worksheet count  
                int worksheetcount = WB.Worksheets.Count;


                StringBuilder logBuilder = new StringBuilder();
                logBuilder.AppendLine(ExcelWorkbookname + " loaded. <br/>");
                logBuilder.AppendLine(ExcelWorkbookname + " has " + worksheetcount + "worksheets");

                Excel.Worksheet abbrWorkSheet =WB.Worksheets["Abbr"];


                //deal with abbreviations
                Excel.Range usedCells = abbrWorkSheet.UsedRange;
                object[,] valueArray = (object[,])usedCells.get_Value(
                                        XlRangeValueDataType.xlRangeValueDefault);
                int rowsLength = valueArray.GetLength(0);
                int columnsLength = valueArray.GetLength(1);


                Dictionary<String, String> abbrSigns = new Dictionary<string, string>();
                //THIS IS NOT 0 INDEXED!!!
                for (int r = 2; r <= rowsLength; r++)
                {
                    string abbrivieatedName = (string)valueArray[r, 2];
                    string fullName = (string)valueArray[r, 1];
                    if (abbrivieatedName == null || fullName == null)
                        continue;
                    if (abbrSigns.ContainsKey(abbrivieatedName))
                        continue;// skip to the next sign if we've seen this sign before
                    abbrSigns.Add(abbrivieatedName, fullName);

                    Sign sign = new Sign();
                    sign.Name = fullName;
                    sign.Type_of_Value = SignTypes.OBSERVATIONAL;

                    CreateSign(context, sign);
                }


                foreach (Excel.Worksheet w in WB.Worksheets)
                {
                    if (w.Name.Equals("Abbr"))
                        continue;
                     usedCells = w.UsedRange;
                     valueArray = (object[,])usedCells.get_Value(
                                            XlRangeValueDataType.xlRangeValueDefault);
                    rowsLength = valueArray.GetLength(0);
                    columnsLength = valueArray.GetLength(1);

                    //we need to deal with the signs first so we can know what the abbrivieations mean once we get the likelihoods from the data

                   
                    if (w.Name.Contains("Disease"))
                    {
                        string prefix = "Disease-Sign";
                        string animalName = w.Name.Replace(prefix, "");
                        logBuilder.AppendLine(animalName + " <br />");

                        //Save  animal names

                        //CreateNewAnimal(context, animalName);

                        for (int r = 2; r <= rowsLength; r++)
                        {
                            if (valueArray[r, 1] == null)
                                continue;
                            Disease d = new Disease();

                            d.Name = (string)valueArray[r, 1]; 
                            //Save disease Name

                           // CreateDisease(context, d);
                            int diseaseID = FindDiseaseIDWithName(context, d.Name);

                            //calculate disease priors for current disease
                            foreach(int id in FindAllAnimalsIDsWithName(context, animalName))
                            {
                                PriorsDiseases pd = new PriorsDiseases();
                                pd.DiseaseID = diseaseID;
                                pd.AnimalID = id;
                                pd.Probability = ((rowsLength / 100.0f)).ToString();   /*we don't do -1 because we add +1 in the end anyway*/
                                CreateDiseasePrior(context, diseaseID, id, pd.Probability);

                               // for (int c = 2; c <= columnsLength; c++)
                               // {
                               //     if (valueArray[r, c] == null || valueArray[1, c] == null)
                               //         continue;
                               //     //Grab Likelihoods from the spreadsheet 
                               //     Likelihood likelihood = new Likelihood();
                               //     likelihood.AnimalId = id;
                               //     likelihood.DiseaseId = diseaseID;
                               //     likelihood.SignId = FindSignIDWithName(context, abbrSigns[(string)valueArray[1, c]]); // find the id using the abbr dictionary
                               //     likelihood.Value = valueArray[r, c].ToString();
                               //     CreateLikelihood(context, likelihood);
                               //}

                          
                            }
                           
                        }

                    }

                    
               }

                TempData["LOG"] = logBuilder.ToString();

                WB.Close();

                Marshal.ReleaseComObject(WB);
                Marshal.ReleaseComObject(app);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return RedirectToAction("Index");

        }

        #endregion
    }
}