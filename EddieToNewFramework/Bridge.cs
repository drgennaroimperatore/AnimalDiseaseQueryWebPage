using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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



            CopyCaseData(CASE_INFO_TABLE);
            CopyCaseData(SET_CASE_TABLE);
            
            
            

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

            

            if (tableName.Equals(SET_CASE_TABLE))
            {
                Console.WriteLine("Copying Cases From " + SET_CASE_TABLE);
               
                
                List<SetCase> setCaseTableRows =  eddie.SetCase.ToList();
                Console.WriteLine("Total Cases: " + setCaseTableRows.Count);

               foreach (SetCase c in setCaseTableRows)
                {
                    Console.WriteLine("Adding Case From " + SET_CASE_TABLE + " ID: " + c.CaseId);

                    Cases newCase = new Cases();
                    newCase.ApplicationVersion = appVersion;
                    newCase.DateOfCaseLogged = currentDate;

                    DateTime caseDate;  DateTime.TryParse(c.Date,  out caseDate);

                    newCase.DateOfCaseObserved = caseDate;

                    newCase.OriginDbname = "Eddie";
                    newCase.OriginTableName = SET_CASE_TABLE;
                    newCase.OriginId = c.CaseId;

                    newCase.Location = c.Location+","+c.Region;
                    newCase.Comments = c.Comment;

                    int animalID = GetAnimalIDBasedOnCaseInfo(c.Species, c.Sex, c.Age);
                    int ownerID = IdentifyOrCreateOwnerOfCase(c.Owner, c.Region, c.Location);

                    newCase.PatientId = IdentifyOrCreateNewPatient(animalID, ownerID);

                    string[] diseaseInfo = GetInfoOnDiseaseChosenByUser(c.UserChdisease);
                    newCase.DiseaseChosenByUserId = GetDiseaseID(diseaseInfo[0]);
                    float likelihoodOfDiseaseChosenByUser; float.TryParse(diseaseInfo[1], out likelihoodOfDiseaseChosenByUser);
                    newCase.LikelihoodOfDiseaseChosenByUser = likelihoodOfDiseaseChosenByUser;

                   

                  
                    ADDB.Add(newCase);
                    ADDB.SaveChanges();

                    Console.WriteLine("Added Case Succesfully");
                }

            }
            else if (tableName.Equals (CASE_INFO_TABLE))
            {
                List<CaseInfo> caseInfoTableRows = eddie.CaseInfo.ToList();
            }


           
            
           



        }



        private int IdentifyOrCreateOwnerOfCase(string name, string region, string location)
        {
            /*this method analyses each case to identify the owner of a case if it doesn't find one it adds a new owner to the owner table*/

            //for the moment since we are copying the data from eddie we will assign the same owner as eddie to all cases

            Owners owner = new Owners();
            owner.Name = name;
            owner.Profession = "Eddie User";

            if (ADDB.Owners.Select(x => x.Name).Contains(name))
               return ADDB.Owners.Where(x => x.Equals(name)).First().Id;

            ADDB.Owners.Add(owner);
            ADDB.SaveChanges();
            int id = ADDB.Owners.Last().Id;
            return id;
        }

        private int GetAnimalIDBasedOnCaseInfo (string name, string sex, string age)
        {
            //for the moment we assign baby to all
            //we need to hard code the names as there are differences in eddie
            string animalNameInEddie = "";
            string sexInEddie = "";
            switch(name)
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

            switch(sex)
            {
                case "Male":
                    sexInEddie = "M";
                    break;

                case "Female":
                    sexInEddie = "F";
                    break;
            }

            /*TODO TRANSLATE EDDIE AGE FORMAT INTO ADDB AGE FORMAT*/
            int animalID = ADDB.Animals.Where(x => x.Name.Equals(animalNameInEddie) && x.Sex.Equals(sexInEddie)).First().Id;

            
            return 0;
        }

        private int IdentifyOrCreateNewPatient(int animalID, int ownerID)
        {
            //this method tries to identify the patient to see if two cases are the same
            //for the moment we'll treat each case as dealing with one patient
            return 0;
        }

        private string[] GetInfoOnDiseaseChosenByUser(string userChDisease)
        {
            //this method parses the userchdisease to extract data on a disease
            return userChDisease.Split(':');
        }

        private int GetDiseaseID(string diseaseName)
        {
           return ADDB.Diseases.Where(x => x.Name.Equals(diseaseName.ToUpper())).First().Id;
        }

        private void GetSymptomsForCaseAndPopulateTable(int caseId, string tableName)
        {
        }

        private int GetInfOnTreatmentChosenByUserOrCreateNewOneIfNotFound (string userChTreatment)
        {
            return 0;
        }

        private void CreateNewTreatment (string drugName, string duration, string dosage, string comments)
        {

        }


    }

    
}
