using AnimalDiseaseQueryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace AnimalDiseaseQueryWebApp.Controllers
{
    public class DiagnoseController : Controller
    {
        Dictionary<Animal, List<Sign>> signsForAnimal = new Dictionary<Animal, List<Sign>>();
        
        // GET: Diagnose
        public ActionResult Index(ADDB context, DiagnoseViewModel model)
        {
           
            model.animals = context.Animals.ToList();

            if(context.SignCore.Count()==0)
                LoadSignsMasterList(context); //load the signcore table if the signcore table is empty

            return View(model);
        }

        public void LoadSignsMasterList(ADDB context)
        {
            string extension = ".xlsx";
            string filename = "master_list";
            string path = Server.MapPath(@"~/Files/" + filename + extension);

            try
            {
                Excel.Application app = new Excel.Application();

                Excel.Workbook WB = app.Workbooks.Open(path);

                Excel.Worksheet signsWorkSheet = WB.Worksheets["Signs core"];

                Excel.Range usedCells = signsWorkSheet.UsedRange;
                object[,] valueArray = (object[,])usedCells.get_Value(
                                        XlRangeValueDataType.xlRangeValueDefault);
                int rowsLength = valueArray.GetLength(0);
                int columnsLength = valueArray.GetLength(1);

                for (int c = 2; c <= columnsLength; c++)
                {

                    string name = (string)valueArray[1, c];
                    if (name == null || name.Equals("Comment"))
                        continue;
                    name = name.ToUpper(); // name needs to be uppercase

                    if(name.Equals("Sheep".ToUpper()))
                    {
                        CreateSignCoresForAnimal(context, "SHEEP", c, rowsLength, valueArray);
                        CreateSignCoresForAnimal(context, "GOAT", c, rowsLength, valueArray);
                    }
                    else if (name.Equals("Equid".ToUpper()))
                    {
                        CreateSignCoresForAnimal(context, "HORSE_MULE", c, rowsLength, valueArray);
                        CreateSignCoresForAnimal(context, "DONKEY", c, rowsLength, valueArray);
                    }
                    else
                    {
                        CreateSignCoresForAnimal(context, name, c, rowsLength, valueArray);
                    }



                }

                //close the workbook and the app 
                WB.Close();

                Marshal.ReleaseComObject(WB);
                Marshal.ReleaseComObject(app);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private void CreateSignCoresForAnimal(ADDB context, string name, int c, int rowsLength,  object[,] valueArray)
        {
             List<int> ids = new List<int>();

            var animals = context.Animals.Where(n => n.Name.Contains(name)).ToList();

            foreach (Animal a in animals)
            {
                for (int r = 2; r <= rowsLength; r++)
                {
                    string signName = (string)valueArray[r, 1];
                    if (signName == null)
                        continue;
                    string columnValue = (string)valueArray[r, c];
                    if (columnValue == null)
                        continue;
                    if (columnValue.Equals("X"))
                    {
                        var signList = context.Signs.Where(s => s.Name.Contains(signName.ToUpper())).ToList();
                        if (signList.Count() > 0)
                        {
                            Sign sign = signList[0];

                            context.SignCore.Add(new SignCore(a.Id, sign.Id));
                        }

                    }
                }
            }
        }

        [HttpPost]
        public ActionResult RenderSignsPartial (ADDB context, int animalID)
        {
            var signcore = context.SignCore.Where(sc => sc.AnimalID == animalID).ToList();
            
            List<Sign> model = new List<Sign>();
            foreach (SignCore sc in signcore)
                model.Add(context.Signs.Find(sc.SignID));

            return PartialView("_SignsList", model);
        }

        public JsonResult DiagnoseAnimal(ADDB context, int animalID, string[] signs)

        {
            if( context.Diseases.Count()==0 ||
                context.Likelihoods.Count()==0 || 
                context.PriorsDiseases.Count()==0 || 
                animalID==-1 ||
                signs== null)
                return Json("Error");

            
            Dictionary<string, float> results = new Dictionary<string, float>();
            var diseases = context.Diseases.ToList();

            foreach(Disease d in diseases)
            {
                if (!CheckIfDiseaseAffectsAnimal(context, animalID, d.Id))
                    continue;
                
                float chainProbability = 1.0f;
                foreach (string s in signs)
                {
                    string[] value = s.Split(new string[] { "+_" }, StringSplitOptions.None);
                    float likelihoodValue = 1.0f; // sign is not observed
                    try
                    {
                        int signID = Convert.ToInt32(value[0]);
                        string signPresence = Convert.ToString(value[1]);

                        //sign is present
                        if (signPresence.Equals("P"))
                        {
                            likelihoodValue = GetLikelihoodValue(context, animalID, signID, d.Id);
                        }
                        //sign is not present
                        else if (signPresence.Equals("NP"))
                        {
                            likelihoodValue = 1.0f - GetLikelihoodValue(context, animalID, signID, d.Id);
                        }

                        //calculate the chain probability 
                        
                        chainProbability *= likelihoodValue;
                       

                    }
                    catch (Exception)
                    {

                    }

                }

                float posterior = chainProbability * GetPriorForDisease(context, animalID, d.Id);
                posterior = (float)Math.Round((double)posterior, 2);
               
               
                results.Add(d.Name, (posterior*100.0f));
            }

            var myList = NormaliseResults(results).ToList();

            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            
            
            return Json(myList);
        }

        private float GetLikelihoodValue(ADDB context, int animalID, int signID, int diseaseID)
        {
            float likelihoodValue = 1.0f;

            var likelihood = context.Likelihoods.Where(m => m.AnimalId == animalID && m.SignId == signID && m.DiseaseId == diseaseID).First();
            float.TryParse(likelihood.Value, out likelihoodValue);
            likelihoodValue /= 100.0f;

            
            return likelihoodValue;
        }

        private float GetPriorForDisease(ADDB context, int animalID, int diseaseID)
        {
            float priorValue = 0.0f;
            var prior = context.PriorsDiseases.Where(m => m.AnimalID == animalID && m.DiseaseID == diseaseID).First();
            float.TryParse(prior.Probability, out priorValue);

            return priorValue;
        }

        public bool CheckIfDiseaseAffectsAnimal(ADDB context, int animalID, int diseaseID)
        {
            return context.PriorsDiseases.Where(m => m.AnimalID == animalID && m.DiseaseID == diseaseID).Count() > 0;
        }

        public Dictionary<string, float> NormaliseResults(Dictionary<string, float> originalList)
        {
            Dictionary<string, float> normalisedList = new Dictionary<string, float>();


            float sumValue = originalList.Values.Sum();

            var keys = originalList.Keys;

            foreach(string k in keys)
            {
                float value = originalList[k];
                float norm = value / sumValue;
                normalisedList.Add(k, norm*100);
            }

            var test = normalisedList.Values.Sum();


            return normalisedList;
        }



    }   
}