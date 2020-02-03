using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace EddieToNewFramework
{
    class AmirTechEddieTod3fBridge: Bridge
    {
        
      
        const string SET_CASE_TABLE = "setCase";
        const string CASE_INFO_TABLE = "caseInfo";
        protected RefactoredEddieContext eddie = new RefactoredEddieContext();




        public class DiseasePredictedByUserReturnValue : ReturnValue
        {
            public float likelihoodOfDisease { get; set; }
        }


        public AmirTechEddieTod3fBridge(string applicationVersion):base(applicationVersion)
        {
          

        }


        public override void TransposeContents()
        {
            /*caseInfo -> diseaseRank
            setCase ->diseaseRankN*/

            /*algorithm pseudo code*/

            /* 1 start with caseInfo table and work in column wise fashion*/


            /*   Treatments dummyTreatment = new Treatments();
               dummyTreatment.Info = "dummy info";
               ADDB.Treatments.Add(dummyTreatment);
               ADDB.SaveChanges();*/

            if (!isInitialised)
            {
                Console.WriteLine("Bridge not initialised. Please call Initialise() before calling Transpose");
                return;
            }




            CopyCaseData(CASE_INFO_TABLE);
            CopyCaseData(SET_CASE_TABLE);

            Console.WriteLine("ALL CASES COPIED PLEASE PRESS ENTER...");


            CleanUp();

        }

        protected override void CopyCaseData(string tableName)
        {
            /* 
             *0) copy comments, and add general information (general info, dates, app version etc.)
             * 1)identify owner or create a new one
             * 2)identify patient or create new patient
             * assign appropriate animal id based on species, sex,age
             * 3) get info on user chosen disease
             * (split percentage and disease)
             * (assign correct id based on disease name)
             * 4) populate symptoms given by user for specific case
             * 5)populate results given by app for specific case
             * 6)parse treatment column to extract name of drugs, duration, dosage 
             * (create new treatment or assign existing treatment id)
             * 
             * 
             */

            //columns are the following
            /* date
             * owner
             * amimalid
             * region
             * location
             * species, age
             * breed
             * sex
             * user chosen disease
             * user chosen treatment
             * comments 
             * comments on treatment
             */

            List<EddieCase> caseData = new List<EddieCase>();

            if (tableName.Equals(SET_CASE_TABLE))
            {
                Console.WriteLine("Copying Cases From " + tableName);

                caseData = eddie.SetCase.Select
                      (c => new EddieCase()
                      {
                          Age = c.Age,
                          AnimalId = c.AnimalId,
                          Date = c.Date,
                          Breed = c.Breed,
                          CaseId = c.CaseId,
                          Comment = c.Comment,
                          CommentTreatment = c.CommentTreatment,
                          Location = c.Location,
                          Owner = c.Owner,
                          Region = c.Region,
                          Sex = c.Sex,
                          Species = c.Species,
                          UserChdisease = c.UserChdisease,
                          UserChTreatment = c.UserChtreatment,
                          

                      }).ToList();

            }
            else if (tableName.Equals(CASE_INFO_TABLE))
            {
                Console.WriteLine("Copying Cases From " + tableName);
                caseData = eddie.CaseInfo.Select
                 (c => new EddieCase()
                 {
                     Age = c.Age,
                     AnimalId = c.AnimalId,
                     Date = c.Date,
                     Breed = c.Breed,
                     CaseId = c.CaseId,
                     Comment = c.Comment,
                     CommentTreatment = c.CommentTreatment,
                     Location = c.Location,
                     Owner = c.Owner,
                     Region = c.Region,
                     Sex = c.Sex,
                     Species = c.Species,
                     UserChdisease = c.UserChdisease,
                     UserChTreatment = c.UserChtreatment

                 }).ToList();


            }
            Console.WriteLine("Total Cases: " + caseData.Count);
            Console.WriteLine("Calculating cases per thread");
            int nOfThreads = 4;
            int batches = (int)Math.Floor((double)caseData.Count / nOfThreads);
            int remainder = caseData.Count % nOfThreads;

            Console.WriteLine("Cases per thread " + batches);
            Console.WriteLine("Remainder " + remainder);

            /*  for (int i = 0; i < nOfThreads; i++)
              {
                  Console.WriteLine("Start: " + i * batches);
                  Console.WriteLine("End: " + (batches + (i * batches) - 1));

                  int startIndex = i * batches;
                  int endIndex = batches + (i * batches) - 1;



              }*/


            ProcessCases(tableName, caseData, 0, 0);




        }


           

  

        private int ProcessCases(string tableName, List<EddieCase> caseData,int startIndex, int endIndex)
        {
            foreach (EddieCase c in caseData)
            {
                int animalID = -1;
                int patientID = -1;
                int ownerID = -1;
                try
                {
                    string logFileStartLine = "Adding Case From " + tableName + " ID: " + c.CaseId;
                    Console.WriteLine(logFileStartLine);

                    if (CheckIfCaseWasAlreadyInserted(c.CaseId, tableName)) // skip if this case was already inserted 
                        continue;
                    // logFileWriter.WriteLine(logFileStartLine);

                    #region GENERAL CASE INFORMATION
                    Cases newCase = new Cases();
                    newCase.ApplicationVersion = appVersion;
                    newCase.DateOfCaseLogged = currentDate;

                    DateTime caseDate;
                    DateTime.TryParse(c.Date, out caseDate);

                    newCase.DateOfCaseObserved = caseDate;

                    newCase.OriginDbname = "Eddie";
                    newCase.OriginTableName = tableName;
                    newCase.OriginId = c.CaseId;

                    newCase.Location = c.Location + "," + c.Region;
                    newCase.Comments = c.Comment;
                    #endregion

                    #region ANIMAL/OWNER INFO
                    animalID = GetAnimalIDBasedOnCaseInfo(c.Species, c.Sex, c.Age);

                    ownerID = IdentifyOrCreateOwnerOfCase(c.Owner, c.Region, c.Location);

                    newCase.PatientId = IdentifyOrCreateNewPatient(animalID, ownerID);
                    
                    #endregion

                    #region INFO ON DISEASE CHOSEN BY USER
                    string[] diseaseInfo = GetInfoOnDiseaseChosenByUser(c.UserChdisease);
                    newCase.DiseaseChosenByUserId = GetDiseaseID(diseaseInfo[0]);
                    float likelihoodOfDiseaseChosenByUser; float.TryParse(diseaseInfo[1].Trim(), out likelihoodOfDiseaseChosenByUser);
                    newCase.LikelihoodOfDiseaseChosenByUser = likelihoodOfDiseaseChosenByUser;
                    newCase.RankOfDiseaseChosenByUser = GetRankOfDiseaseChosenByUser(c.CaseId, tableName);
                    #endregion

                    ReturnValue DiseasePredictedByApp = GetInfoOnDiseasePredictedByAppRankedFirst(c.CaseId, tableName);
                    if (DiseasePredictedByApp.ThereWasAnError)
                    {
                        CleanUpNewPatientAndNewOwnerAsAResultOfAnError(patientID, ownerID);
                        continue;
                    }

                    newCase.DiseasePredictedByAppId = DiseasePredictedByApp.ID;



                    #region INFO ON TREATMENT CHOSEN BY USER
                    //!!!TO DO CHANGE THIS WHEN TREATMENT DESIGH IS COMPLETE!!!!!
                    newCase.TreatmentChosenByUserId = ADDB.Treatments.Last().Id;
                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//////////////////
                    #endregion


                    ADDB.Add(newCase);
                    ADDB.SaveChanges();
                }
                catch (Exception e)
                {
                    logFileWriter.WriteLine("Problem With Case " + c.CaseId + "from " + tableName);
                    logFileWriter.WriteLine("Excpetion details: {0} {1}", e.Message, e.StackTrace);

                    if (ADDB.Patients.Count() > 0)
                    {
                        patientID = ADDB.Patients.Last().Id;
                        patientID = ADDB.Owners.Last().Id;
                        CleanUpNewPatientAndNewOwnerAsAResultOfAnError(patientID, ownerID);
                    }
                    continue;
                }

                //WARNING THE SYMPTOMS AND RESULTS TABLE FUNCTION NEED THE NEW CASE ID (SO WE MUST UPDATE THE CASE TABLE FIRST AND THEN CALL THE FUNCTIONS


                #region POPULATE THE SIGNS FOR CASE TABLE
                int currentCaseID = ADDB.Cases.Last().Id; // get the id of the latest case
                GetSymptomsForCaseAndPopulateTable(c.CaseId, currentCaseID, tableName);
                #endregion

                #region INFO ON RESULTS OF THE CASE
                ReturnValue ResultsOfTheCaseInsertionOutcome = GetResultsForCaseAndPopulateTable(c.CaseId, currentCaseID, tableName);
                if (ResultsOfTheCaseInsertionOutcome.ThereWasAnError)
                    continue;
                #endregion

                Console.WriteLine("Added Case Succesfully");
                // logFileWriter.WriteLine("Added Case {0} from {1} Successfully", c.CaseId, tableName);
            }
            return 1;
        }

        protected override bool CheckIfCaseWasAlreadyInserted(int originalCaseID, string tableName)
        {
            bool caseIsAlreadyPresent = false;

            try
            {
               var duplicateCases= ADDB.Cases.Where(x => x.OriginId == originalCaseID && x.OriginTableName.Equals(tableName)).ToList();
               caseIsAlreadyPresent = duplicateCases.Count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Stop Execution there is a problem with the duplication checking method");
            }

            return caseIsAlreadyPresent;
        }

        protected override void CleanUpNewPatientAndNewOwnerAsAResultOfAnError(int patientID, int ownerID)
        {
            if (patientID != -1)
            {
                var patientToRemove = ADDB.Patients.Find(patientID);
                ADDB.Patients.Remove(patientToRemove);
            }

            if (ownerID != -1)
            {
                var ownerToRemove = ADDB.Owners.Find(ownerID);
                ADDB.Owners.Remove(ownerToRemove);
            }

            ADDB.SaveChanges();
        }

        protected override int IdentifyOrCreateOwnerOfCase(string name, string region, string location)
        {
            /*this method analyses each case to identify the owner of a case if it doesn't find one it adds a new owner to the owner table*/

            //for the moment since we are copying the data from eddie we will assign the same owner as eddie to all cases

            Owners owner = new Owners();
            owner.Name = name;
            owner.Profession = "Eddie User";

            /* if (ADDB.Owners.Select(x => x.Name).Contains(name))
                return ADDB.Owners.Where(x => x.Equals(name)).First().Id;*/

            ADDB.Owners.Add(owner);
            ADDB.SaveChanges();
            int id = ADDB.Owners.Last().Id;
            return id;
        }

        

        private int IdentifyOrCreateNewPatient(int animalID, int ownerID)
        {
            //this method tries to identify the patient to see if two cases are the same
            //for the moment we'll treat each case as dealing with one patient

            Patients newPatient = new Patients();
            newPatient.AnimalId = animalID;
            newPatient.OwnerId = ownerID;
            ADDB.Patients.Add(newPatient);
            ADDB.SaveChanges();
            int newPatientID = ADDB.Patients.Last().Id;

            return newPatientID;
        }

        private string[] GetInfoOnDiseaseChosenByUser(string userChDisease)
        {
            //this method parses the userchdisease to extract data on a disease
            string preprocessedString = userChDisease.Replace('%', ' ').Trim();
            string[] result = preprocessedString.Split(':');
            result[0] = result[0].Trim();
            return result;
        }

       
        private int GetRankOfDiseaseChosenByUser(int caseId, string tableName)
        {
            bool thereWasAProblem = false;
            string diseaseRankTableName = "";
            int result = -1;

            if (tableName.Equals(SET_CASE_TABLE))
            {
                //disease n (same rule applies join with newer table)
                diseaseRankTableName = "diseaseRankN";

            }
            else if (tableName.Equals(CASE_INFO_TABLE))
            {
                //diseaseRank
                diseaseRankTableName = "diseaseRank";

            }


            string rawSQL = "SELECT " 
                + diseaseRankTableName + ".percentage," 
                + diseaseRankTableName + ".diseaseName," 
                + diseaseRankTableName + ".caseID,"
                + diseaseRankTableName + ".ID," 
                + diseaseRankTableName+".rank"+
                " FROM " + diseaseRankTableName + "," + tableName +
                " WHERE " + diseaseRankTableName + ".caseID=@p0 " +
                "AND " + tableName + ".caseID=@p0 " +
                "AND " + diseaseRankTableName + ".diseaseName= (SELECT SUBSTRING_INDEX("+tableName+ ".userCHdisease, ':', 1) as result) " +
                "ORDER BY rank ASC";

            try
            {
#pragma warning disable EF1000 // Possible SQL injection vulnerability.

                if (tableName.Equals(SET_CASE_TABLE))
                {
                    List<DiseaseRankN> diseaseRankNs = eddie.DiseaseRankN.FromSql(rawSQL, caseId).ToList();
                    result = eddie.DiseaseRankN.FromSql(rawSQL, caseId).ToList()[0].Rank;
                }
                else if (tableName.Equals(CASE_INFO_TABLE))
                {
                    List<DiseaseRank> diseaseRankNs = eddie.DiseaseRank.FromSql(rawSQL, caseId).ToList();
                    result = eddie.DiseaseRank.FromSql(rawSQL, caseId).ToList()[0].Rank;
                   
                }



                }

            catch(Exception e )
            {
                thereWasAProblem = true;
              
                logFileWriter.WriteLine("Problem with {0} in method GetRankOfDiseaseChosenByUser from table {1}", caseId, tableName);
                logFileWriter.WriteLine("Excpetion details: {0} {1}", e.Message, e.StackTrace);
            }

            return result;
        }

        private DiseasePredictedByUserReturnValue GetInfoOnDiseasePredictedByAppRankedFirst(int caseId, string tableName)
        {
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            int mostLikelyDiseaseAccordingToAppId = 0;
            bool thereWasAProblem = false;
            float mostLikelyDiseaseAccordingToAppLikelihood = -1.0f;
            try
            {

                string diseaseRankTableName = "";
                if (tableName.Equals(SET_CASE_TABLE))
                {
                    //disease n (same rule applies join with newer table)
                    diseaseRankTableName = "diseaseRankN";

                }
                else if (tableName.Equals(CASE_INFO_TABLE))
                {
                    //diseaseRank
                    diseaseRankTableName = "diseaseRank";

                }

                string rawSQL = "SELECT * " +
                    "FROM " + diseaseRankTableName +
                    " WHERE caseID = @p0 " +
                    "ORDER BY rank ASC";


                if (tableName.Equals(SET_CASE_TABLE))
                {
                    var ranks = eddie.DiseaseRankN.FromSql(rawSQL, caseId).ToList();
                    var firstRanked = ranks.First();
                    string diseaseName = firstRanked.DiseaseName.Trim();
                   float.TryParse(firstRanked.Percentage, out mostLikelyDiseaseAccordingToAppLikelihood);
                    mostLikelyDiseaseAccordingToAppId = GetDiseaseID(diseaseName);

                }
                else if (tableName.Equals(CASE_INFO_TABLE))
                {
                    var ranks = eddie.DiseaseRank.FromSql(rawSQL, caseId).ToList();
                    var firstRanked = ranks.First();
                    string diseaseName = firstRanked.DiseaseName.Trim();
                    float.TryParse(firstRanked.Percentage, out mostLikelyDiseaseAccordingToAppLikelihood);
                    mostLikelyDiseaseAccordingToAppId = GetDiseaseID(diseaseName);
                }
            }
            catch (Exception e)
            {
                thereWasAProblem = true;
                logFileWriter.WriteLine("Problem with {0} in method GetDiseaseIDPredictedbyAppRankedFirst from table {1}", caseId, tableName);
                logFileWriter.WriteLine("Excpetion details: {0} {1}", e.Message,e.StackTrace);
            }


            return 
                new DiseasePredictedByUserReturnValue
                {
                    ID = mostLikelyDiseaseAccordingToAppId,
                    likelihoodOfDisease = mostLikelyDiseaseAccordingToAppLikelihood,
                    ThereWasAnError = thereWasAProblem
                };
        }

        
      
        protected override ReturnValue GetSymptomsForCaseAndPopulateTable(int originalCaseId, int newCaseId, string tableName)
        {
            bool thereWasAnError = false;
            try
            {

                if (tableName.Equals(SET_CASE_TABLE))
                {
                    //selected symptoms n
                    //get the symptoms for the specific case
                    List<SignForCases> syptoms = eddie.SelectedSymptomsN.Where(x => x.CaseId == originalCaseId).Select(x =>  new SignForCases { CaseId = newCaseId, SignId = GetSignID(x.SymptomName), SignPresence= TranslateSymptomPresenceFromEddieToNewFrameWork(x.Selection) }).ToList();
                    ADDB.SignForCases.AddRange(syptoms);


                }
                else if (tableName.Equals(CASE_INFO_TABLE))
                {
                    //selected symptoms
                    List<SignForCases> syptoms = eddie.SelectedSymptoms.Where(x => x.CaseId == originalCaseId).Select(x => new SignForCases { CaseId = newCaseId, SignId = GetSignID(x.SymptomName), SignPresence = TranslateSymptomPresenceFromEddieToNewFrameWork(x.Selection) }).ToList(); ;
                    ADDB.SignForCases.AddRange(syptoms);
                }
                ADDB.SaveChanges();
            }
            catch (Exception e)
            {
                logFileWriter.WriteLine("Problem with {0} in method GetDiseaseSymptomsforCaseAndPopulateTable from table {1}", originalCaseId, tableName);
                logFileWriter.WriteLine("Excpetion details: {0} {1}", e.Message, e.StackTrace);
            }
            Console.WriteLine("Succesfully added signs for case {0}: {1} ", tableName, originalCaseId);
            return new ReturnValue { ID = 0, ThereWasAnError = thereWasAnError };
        }

        private float ConvertLikelihoodFromStringToFloat(string likelihood)
        {
            float fLikelihood;
            float.TryParse(likelihood, out fLikelihood);
            return fLikelihood;

        }

        protected override ReturnValue GetResultsForCaseAndPopulateTable (int originalCaseId, int newCaseId, string tableName)
        {
            bool thereWasAnError = false;
            try
            {
                if (tableName.Equals(SET_CASE_TABLE))
                {
                    //diseaseRank n
                    //get the symptoms for the specific case
                    List<ResultForCases> results = eddie.DiseaseRankN
                        .Where(x => x.CaseId == originalCaseId)
                        .Select(x => 
                        new ResultForCases
                        {
                            CaseId = newCaseId,
                            DiseaseId =GetDiseaseID(x.DiseaseName),
                            PredictedLikelihoodOfDisease = ConvertLikelihoodFromStringToFloat(x.Percentage)
                        }).ToList();
                    ADDB.ResultForCases.AddRange(results);


                }
                else if (tableName.Equals(CASE_INFO_TABLE))
                {
                    //diseaseRank
                    List<ResultForCases> results = eddie.DiseaseRank
                        .Where(x => x.CaseId == originalCaseId)
                        .Select(x => new ResultForCases
                        {
                            CaseId = newCaseId,
                            DiseaseId = GetDiseaseID(x.DiseaseName),
                            PredictedLikelihoodOfDisease = ConvertLikelihoodFromStringToFloat(x.Percentage)
                        }).ToList(); 
                    ADDB.ResultForCases.AddRange(results);
                }
                ADDB.SaveChanges();

            }
            catch(Exception e)
            {
                thereWasAnError = true;
                logFileWriter.WriteLine("Problem in method GetResultsForCaseAndPopulateTable() method for case {0} in table {1}", originalCaseId, tableName);
                logFileWriter.WriteLine("Excpetion details: {0} {1}", e.Message, e.StackTrace);

            }

            Console.WriteLine("Sucessfully added results for case {0} from table {1}", originalCaseId, tableName);

            return new ReturnValue { ID=0, ThereWasAnError= thereWasAnError };
        }

        protected override int GetInfOnTreatmentChosenByUserOrCreateNewOneIfNotFound(string userChTreatment)
        {
            return 0;
        }

        private void CreateNewTreatment(string drugName, string duration, string dosage, string comments)
        {

        }

      
    }


}
