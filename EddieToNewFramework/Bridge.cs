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


        public Bridge()
        {
            Console.WriteLine("Bridge console application 1.0");

        }

        public void Initialise()
        {
            Console.WriteLine("Initialisation method");
            int c =eddie.SetCase.Count();
            Console.WriteLine("SetCase Count " + c);
        }

        public void TransposeContents()
        {
            /*caseInfo -> diseaseRank
            setCase ->diseaseRankN*/

            /*algorithm pseudo code*/

            /* 1 start with caseInfo table and work in column wise fashion
             
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



        }



        private int IdentifyOrCreateOwnerOfCase(string name, string region, string location)
        {
            /*this method analyses each case to identify the owner of a case if it doesn't find one it adds a new owner to the owner table*/

            //for the moment since we are copying the data from eddie we will assign the same owner to all cases


            return 0;
        }

        private int GetAnimalIDBasedOnCaseInfo (string name, string sex, string age)
        {
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
            return new string[] { };
        }

        private int GetDiseaseID(string diseaseName)
        {
            return 0;
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
