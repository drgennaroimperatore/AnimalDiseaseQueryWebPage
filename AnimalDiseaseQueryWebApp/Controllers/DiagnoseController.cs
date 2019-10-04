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

            if (context.SignCore.Count() == 0)
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

                    if (name.Equals("Sheep".ToUpper()))
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

        private void CreateSignCoresForAnimal(ADDB context, string name, int c, int rowsLength, object[,] valueArray)
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
        public ActionResult RenderSignsPartial(ADDB context, int animalID)
        {
            var signcore = context.SignCore.Where(sc => sc.AnimalID == animalID).ToList();

            List<Sign> model = new List<Sign>();
            foreach (SignCore sc in signcore)
                model.Add(context.Signs.Find(sc.SignID));

            return PartialView("_SignsList", model);
        }

        public JsonResult DiagnoseAnimal(ADDB context, int animalID, string[] signs)

        {
            if (context.Diseases.Count() == 0 ||
                context.Likelihoods.Count() == 0 ||
                context.PriorsDiseases.Count() == 0 ||
                animalID == -1 ||
                signs == null)
                return Json("Error");


            Dictionary<string, float> results = new Dictionary<string, float>();
            var diseases = context.Diseases.ToList();

            foreach (Disease d in diseases)
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



                results.Add(d.Name, (posterior * 100.0f));
            }



            return Json(NormaliseResults(results));
        }

        public JsonResult LogCase(ADDB context,
            int animalID,
            string[] signs,
            Dictionary<string, string> results,
            string diseasechosenbyuser,
            string region,
            string location,
            DateTime datecaseobserved,
            string comments)
        {
            string r = "Case Added Sucessfully";

            string name = "temporary name";

            // Create owner

            int ownerID = IdentifyOrCreateOwnerOfCase(context, name, region, location);


            try
            {

                //create patient
                Patient newPatient = new Patient();

                newPatient.AnimalID = animalID;
                newPatient.OwnerID = ownerID;

                context.Patients.Add(newPatient);

                context.SaveChanges();

                int patientID = context.Patients.ToList().Last().ID;


                //create case

                Case newCase = new Case();

                newCase.Location = location + "," + region;

                newCase.PatientID = patientID;

                //get info about the disease chosen by the user(we'll call this dbu)
                //the data is formatted rank_name_likelihoodvalue %
                string[] dbu = diseasechosenbyuser.Split('_');
                int rank = Convert.ToInt32(dbu[0]);
                string dbuName = dbu[1]; string dbuLikelihood = dbu[2];
                dbuLikelihood = dbuLikelihood.Remove(dbuLikelihood.Length - 1, 1); // remove the percentage symbol
                float dbuLikelihoodVal; float.TryParse(dbuLikelihood, out dbuLikelihoodVal);


                newCase.DiseasePredictedByAppID = GetDiseaseID(context, results.Keys.First());
                newCase.DiseaseChosenByUserID = GetDiseaseID(context, dbuName);
                newCase.LikelihoodOfDiseaseChosenByUser = dbuLikelihoodVal;
                newCase.RankOfDiseaseChosenByUser = rank;



                newCase.Comments = comments;
                newCase.DateOfCaseObserved = datecaseobserved;
                newCase.DateOfCaseLogged = DateTime.Now;

                #region TEMPORARY DUMMY TREATMENT CODE!!!!!!!! TO DO!! FIX!!!
                int treatmentID = context.Treatments.ToList().Last().Id; // this dummy treatment was created during conversion of eddie cases...
                newCase.TreatmentChosenByUserID = treatmentID;

                #endregion



                newCase.ApplicationVersion = "1.0";
                newCase.OriginTableName = "Cases";
                newCase.OriginDBName = "D3FFramework";



                context.Cases.Add(newCase);

                context.SaveChanges();
            }
            catch (Exception)
            {
                r = "There was an error logging the case";
            }

            try
            {

                int caseID = context.Cases.ToList().Last().ID;

                //log signs

                foreach (string sign in signs)
                {
                    string[] splitSymbol = { "+_" };
                    string[] s = sign.Split(splitSymbol, StringSplitOptions.None);

                    int signID = Convert.ToInt32(s[0]);
                    string signPresence = s[1];
                    SignForCase signForCase = new SignForCase();
                    signForCase.CaseID = caseID;
                    signForCase.SignID = signID;

                    switch (signPresence)
                    {
                        case "P":
                            signForCase.SignPresence = SignPresence.PRESENT;
                            break;
                        case "NP":
                            signForCase.SignPresence = SignPresence.NOT_PRESENT;
                            break;
                        case "NO":
                            signForCase.SignPresence = SignPresence.NOT_OBSERVED;
                            break;
                    }

                    context.SignsForCases.Add(signForCase);
                }

                //log results

                foreach (string result in results.Keys)
                {
                    ResultForCase resultForCase = new ResultForCase();
                    resultForCase.CaseID = caseID;
                    resultForCase.DiseaseID = GetDiseaseID(context, result);
                    //convert predicted likelihood of case //and remove last char because it's the perc symbol
                    float pl; float.TryParse(results[result].Remove(results[result].Length - 1, 1), out pl);
                    resultForCase.PredictedLikelihoodOfDisease = pl;

                    context.ResultsForCases.Add(resultForCase);
                }
                context.SaveChanges();
            }

            catch (Exception e)
            {
                r = "There was an error logging the case";
                //to do... write some code to clean up incomplete logged cases.... 
            }

            return Json(r);
        }

        public JsonResult DeleteIncompleteCase(ADDB context, int caseID)
        {
            string r = "";

            //check if there have been symptoms log but no results
            var signsForCase = context.SignsForCases.Where(x => x.CaseID == caseID).ToList();
            if (signsForCase.Count > 0)
            {

                //delete the symps
                context.SignsForCases.RemoveRange(signsForCase);

                context.SaveChanges();
            }



            //finally delete the case
            context.Cases.Remove(context.Cases.Find(caseID));

            context.SaveChanges();




            return Json(r);
        }



        #region ACCESSORY FUNCTIONS

        private int GetDiseaseID(ADDB context, string disease)
        {
            return context.Diseases.Where(x => x.Name.Equals(disease)).First().Id;
        }

        private int GetSignID(ADDB context, string signName)
        {
            return context.Signs.Where(x => x.Name.Equals(signName)).First().Id;
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

        public Dictionary<string, string> NormaliseResults(Dictionary<string, float> originalList)
        {
            List<KeyValuePair<string, float>> normalisedList = new List<KeyValuePair<string, float>>();
            Dictionary<string, string> result = new Dictionary<string, string>();

            float sumValue = originalList.Values.Sum();

            var keys = originalList.Keys;

            foreach (string k in keys)
            {
                float value = originalList[k];
                float norm = (value / sumValue);
                normalisedList.Add(new KeyValuePair<string, float>(k, (norm * 100)));
            }


            normalisedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            normalisedList.ForEach(x => result.Add(x.Key, " " + x.Value.ToString("f2") + "%"));

            return result;
        }


        private int IdentifyOrCreateOwnerOfCase(ADDB context, string name, string region, string location)
        {
            /*this method analyses each case to identify the owner of a case if it doesn't find one it adds a new owner to the owner table*/

            //for the moment since we are copying the data from eddie we will assign the same owner as eddie to all cases

            Owner owner = new Owner();
            owner.Name = name;
            owner.Profession = "Eddie User";

            /* if (ADDB.Owners.Select(x => x.Name).Contains(name))
                return ADDB.Owners.Where(x => x.Equals(name)).First().Id;*/

            context.Owners.Add(owner);
            context.SaveChanges();
            int id = context.Owners.ToList().Last().ID;
            return id;
        }

        #endregion


    }
}