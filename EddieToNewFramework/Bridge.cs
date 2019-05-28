using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EddieToNewFramework
{
    class Bridge
    {
        RefactoredEddieContext eddie = new RefactoredEddieContext();
        TestFrameworkContext ADDB = new TestFrameworkContext();
        const string SET_CASE_TABLE = "setCase";
        const string CASE_INFO_TABLE = "caseInfo";

        DateTime currentDate = DateTime.Now.Date;
        string appVersion = "";


        public Bridge(string applicationVersion)
        {
            Console.WriteLine("Bridge console application 1.0");
            appVersion = applicationVersion;

        }

        public void Initialise()
        {

        }

        public void TransposeContents()
        {
            /*caseInfo -> diseaseRank
            setCase ->diseaseRankN*/

            /*algorithm pseudo code*/

            /* 1 start with caseInfo table and work in column wise fashion*/


         /*   Treatments dummyTreatment = new Treatments();
            dummyTreatment.Info = "dummy info";
            ADDB.Treatments.Add(dummyTreatment);
            ADDB.SaveChanges();*/


            CopyCaseData(CASE_INFO_TABLE);
           // CopyCaseData(SET_CASE_TABLE);




        }

        public void CopyCaseData(string tableName)
        {
            /* 
             *
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
             * 7) copy comments if any
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
                Console.WriteLine("Copying Cases From " + SET_CASE_TABLE);

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
                          UserChTreatment = c.UserChtreatment

                      }).ToList();

            }
            else if (tableName.Equals(CASE_INFO_TABLE))
            {
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

            foreach (EddieCase c in caseData)
            {

                Console.WriteLine("Adding Case From " + SET_CASE_TABLE + " ID: " + c.CaseId);

                #region GENERAL CASE INFORMATION
                Cases newCase = new Cases();
                newCase.ApplicationVersion = appVersion;
                newCase.DateOfCaseLogged = currentDate;

                DateTime caseDate;
                DateTime.TryParse(c.Date, out caseDate);

                newCase.DateOfCaseObserved = caseDate;

                newCase.OriginDbname = "Eddie";
                newCase.OriginTableName = SET_CASE_TABLE;
                newCase.OriginId = c.CaseId;

                newCase.Location = c.Location + "," + c.Region;
                newCase.Comments = c.Comment;
                #endregion

                #region ANIMAL/OWNER INFO
                int animalID = GetAnimalIDBasedOnCaseInfo(c.Species, c.Sex, c.Age);

                int ownerID = IdentifyOrCreateOwnerOfCase(c.Owner, c.Region, c.Location);

                newCase.PatientId = IdentifyOrCreateNewPatient(animalID, ownerID);
                #endregion

                #region INFO ON DISEASE CHOSEN BY USER
                string[] diseaseInfo = GetInfoOnDiseaseChosenByUser(c.UserChdisease);
                newCase.DiseaseChosenByUserId = GetDiseaseID(diseaseInfo[0]);
                float likelihoodOfDiseaseChosenByUser; float.TryParse(diseaseInfo[1], out likelihoodOfDiseaseChosenByUser);
                newCase.LikelihoodOfDiseaseChosenByUser = likelihoodOfDiseaseChosenByUser;
                #endregion

                newCase.DiseasePredictedByAppId = GetDiseaseIDPredictedByAppRankedFirst(c.CaseId, tableName); // a dummy id as we need a join to get the disease and the likelihood of disease from disease rank

                #region INFO ON TREATMENT CHOSEN BY USER
                //!!!TO DO CHANGE THIS WHEN TREATMENT DESIGH IS COMPLETE!!!!!
                newCase.TreatmentChosenByUserId = ADDB.Treatments.Last().Id;
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//////////////////
                #endregion


                ADDB.Add(newCase);
                ADDB.SaveChanges();

                //WARNING THE SYMPTOMS AND RESULTS TABLE FUNCTION NEED THE NEW CASE ID (SO WE MUST UPDATE THE CASE TABLE FIRST AND THEN CALL THE FUNCTIONS


                #region POPULATE THE SIGNS FOR CASE TABLE
                int currentCaseID = ADDB.Cases.Last().Id; // get the id of the latest case
                GetSymptomsForCaseAndPopulateTable(c.CaseId,currentCaseID, tableName);
                #endregion  








               

                Console.WriteLine("Added Case Succesfully");
            }



        }


        private int IdentifyOrCreateOwnerOfCase(string name, string region, string location)
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

        private int GetAnimalIDBasedOnCaseInfo(string name, string sex, string age)
        {
            //for the moment we assign baby to all
            //we need to hard code the names as there are differences in eddie
            string animalNameInEddie = "";
            string sexInEddie = "";
            switch (name)
            {
                case "Cattle":
                    animalNameInEddie = "CATTLE";
                    break;

                case "Horse/Mule":
                    animalNameInEddie = "HORSE_MULE";
                    break;

                case "Donkey":
                    animalNameInEddie = "DONKEY";
                    break;

                case "Camel":
                    animalNameInEddie = "CAMEL";
                    break;

                case "Sheep":
                    animalNameInEddie = "SHEEP";
                    break;

                case "Goats":
                    animalNameInEddie = "GOAT";
                    break;

            }

            switch (sex)
            {
                case "Male":
                    sexInEddie = "M";
                    break;

                case "Female":
                    sexInEddie = "F";
                    break;
            }

            /*TODO TRANSLATE EDDIE AGE FORMAT INTO ADDB AGE FORMAT*/

            int animalID = ADDB.Animals.Where(x => x.Name.Trim().Equals(animalNameInEddie) && x.Sex == sexInEddie).ToList().First().Id;

            return animalID;
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
            result[0] = result[0].Trim().ToUpper();
            return result;
        }

        private int GetDiseaseID(string diseaseName)
        {
            return ADDB.Diseases.Where(x => x.Name.Equals(diseaseName.ToUpper())).First().Id;
        }

        public int GetDiseaseIDPredictedByAppRankedFirst(int caseId, string tableName)
        {
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            int mostLikelyDiseaseAccordingToAppId = 0;
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
                string diseaseName = firstRanked.DiseaseName.Trim().ToUpper();
                mostLikelyDiseaseAccordingToAppId = GetDiseaseID(diseaseName);

            }
            else if (tableName.Equals(CASE_INFO_TABLE))
            {
                var ranks = eddie.DiseaseRank.FromSql(rawSQL, caseId).ToList();
                var firstRanked = ranks.First();
                string diseaseName = firstRanked.DiseaseName.Trim().ToUpper();
                mostLikelyDiseaseAccordingToAppId = GetDiseaseID(diseaseName);
            }


            return mostLikelyDiseaseAccordingToAppId;
        }



        private int  GetSignID(string signName)
        {
            return 4; // dummy result for first sign to test the population of get symptomsforcaseandpopulatetable
        }


        private void GetSymptomsForCaseAndPopulateTable(int originalCaseId, int newCaseId, string tableName)
        {

            if (tableName.Equals(SET_CASE_TABLE))
            {
                //selected symptoms n
                //get the symptoms for the specific case
                List<SignForCases> syptoms = eddie.SelectedSymptomsN.Select(x=> new SignForCases {CaseId= newCaseId, SignId = GetSignID(x.SymptomName)}).Where(x => x.CaseId == originalCaseId).ToList();
                ADDB.SignForCases.AddRange(syptoms);
                

            }
            else if (tableName.Equals(CASE_INFO_TABLE))
            {
                //selected symptoms
                List<SignForCases> syptoms = eddie.SelectedSymptoms.Select(x => new SignForCases { CaseId = newCaseId, SignId = GetSignID(x.SymptomName) }).Where(x => x.CaseId == originalCaseId).ToList();
                ADDB.SignForCases.AddRange(syptoms);
            }
            ADDB.SaveChanges();
        }

        private int GetInfOnTreatmentChosenByUserOrCreateNewOneIfNotFound(string userChTreatment)
        {
            return 0;
        }

        private void CreateNewTreatment(string drugName, string duration, string dosage, string comments)
        {

        }


    }


}
