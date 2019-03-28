using AnimalDiseaseQueryWebApp.Models;
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

            if (context.Diseases.Where(d => d.Name == disease.Name).Count() > 0)
            {
                TempData["Errors"] = "Disease " + disease.Name + " already exists!";
                return;
            }

            context.Diseases.Add(disease);

            context.SaveChanges();

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
            //deal with duplicate entry
            var duplicate = context.PriorsDiseases.Where(m => m.DiseaseID == diseaseID && m.AnimalID == animalID).ToList();
            if (duplicate.Count > 0)
            {
                duplicate[0].Probability = probability;
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



                foreach (Excel.Worksheet w in WB.Worksheets)
                {
                    Excel.Range usedCells = w.UsedRange;
                    object[,] valueArray = (object[,])usedCells.get_Value(
                                            XlRangeValueDataType.xlRangeValueDefault);
                    int rowsLength = valueArray.GetLength(0);
                    int columnsLength = valueArray.GetLength(1);

                    //we need to deal with the signs first so we can know what the abbrivieations mean once we get the likelihoods from the data

                    if (w.Name.Equals("Abbr"))
                    {
                        //deal with abbreviations



                        Dictionary<String, String> abbrSigns = new Dictionary<string, string>();
                        //THIS IS NOT 0 INDEXED!!!
                        for (int r = 2; r < rowsLength - 1; r++)
                        {
                            string abbrivieatedName = (string)valueArray[r, 2];
                            string fullName = (string)valueArray[r, 1];
                            abbrSigns.Add(abbrivieatedName, fullName);

                            Sign sign = new Sign();
                            sign.Name = fullName;
                            sign.Type_of_Value = SignTypes.OBSERVATIONAL;

                            CreateSign(context, sign);
                        }


                    }


                    if (w.Name.Contains("Disease"))
                    {
                        string prefix = "Disease-Sign";
                        string animalName = w.Name.Replace(prefix, "");
                        logBuilder.AppendLine(animalName + " <br />");

                        //Save  animal names

                        //CreateNewAnimal(context, animalName);

                        for (int r = 2; r < rowsLength - 1; r++)
                        {
                            Disease d = new Disease();
                            d.Name = (string)valueArray[r, 1]; 
                            //Save disease Name

                            CreateDisease(context, d);
                            int diseaseID = context.Diseases.Last().Id;

                            //calculate disease priors for current disease
                            foreach(int id in FindAllAnimalsIDsWithName(context, animalName))
                            {
                                PriorsDiseases pd = new PriorsDiseases();
                                pd.DiseaseID = diseaseID;
                                pd.AnimalID = id;
                                pd.Probability = (rowsLength / 100).ToString();   /*we don't do -1 because we add +1 in the end anyway*/

                                //Grab Likelihoods from the spreadsheet 
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