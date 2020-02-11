using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EddieToNewFramework
{
    abstract class Bridge
    {

        public class ReturnValue
        {
            public int ID { get; set; }
            public bool ThereWasAnError { get; set; }
        }

        public class ResultReturnValue: ReturnValue
        {
            public List<ResultForCases> resultForCase { get; set; }
        }

        

        protected TestFrameworkContext ADDB;
        

        protected Dictionary<string, string> symptomLookupDictionary = new Dictionary<string, string>();
        protected Dictionary<string, string> diseaseLookupDictionary = new Dictionary<string, string>();

       protected StreamWriter logFileWriter;
       protected FileStream logFileStream;

       protected bool isInitialised = false;

        protected string appVersion = "";
        protected DateTime currentDate = DateTime.Now.Date;

        public Bridge(string applicationVersion)
        {
            Console.WriteLine("Bridge console application 1.0");
            appVersion = applicationVersion;
        }

        public virtual void Initialise()
        {
            ADDB = new TestFrameworkContext();

            string symptomLookupath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SYMP_LOOKUP.txt");
            string diseaseLookupath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\DIS_LOOKUP.txt");

            string logFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\LOG_" + DateTime.Now.ToString("yyyy-mm-ddAThh-mm-ss") + ".txt");

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(symptomLookupath))
                {
                    string line = "";
                    // Read the stream to a string, and write the string to the console.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineContent = line.Split(',');
                        symptomLookupDictionary.Add(lineContent[0], lineContent[1]);
                    }
                }


                using (StreamReader sr = new StreamReader(diseaseLookupath))
                {
                    string line = "";
                    // Read the stream to a string, and write the string to the console.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineContent = line.Split(',');
                        diseaseLookupDictionary.Add(lineContent[0], lineContent[1]);
                    }
                }

                //initialise log writer 
                logFileStream = new FileStream(logFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                logFileWriter = new StreamWriter(logFileStream);

                isInitialised = true;
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public abstract void TransposeContents();
        protected abstract void CopyCaseData(string tableName);
        protected abstract bool CheckIfCaseWasAlreadyInserted(int originalCaseID, string tableName);
        protected abstract void CleanUpNewPatientAndNewOwnerAsAResultOfAnError(int patientID, int ownerID);
        protected abstract int IdentifyOrCreateOwnerOfCase(string name, string region, string location);
        protected abstract int IdentifyOrCreateNewPatient(int animalID, int ownerID);

        protected int GetAnimalIDBasedOnCaseInfo(string name, string sex, string age)
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

        protected int GetDiseaseID(string diseaseName)
        {
            string lookup = "";
            try
            {
                lookup = diseaseLookupDictionary[diseaseName];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(diseaseName + " not Found in disease lookup dictionary");
            }

            return ADDB.Diseases.Where(x => x.Name.Equals(lookup)).First().Id;
        }

        protected int GetSignID(string signName)
        {
            string symptomName = symptomLookupDictionary[signName.Trim()];
            return ADDB.Signs.Where(x => x.Name.Equals(symptomName)).First().Id;
        }


        protected string TranslateSymptomPresenceFromEddieToNewFrameWork(string eddieSymptomPresence)
        {
            string presence = "";

            switch (eddieSymptomPresence)
            {
                case "Absent":
                    presence = "NP";
                    break;
                case "Present":
                    presence = "P";
                    break;
                case "Unknown":
                    presence = "NO";
                    break;
            }

            return presence;
        }

        protected abstract ReturnValue GetSymptomsForCaseAndPopulateTable(int originalCaseId, int newCaseId, string tableName);
        protected abstract ReturnValue GetResultsForCaseAndPopulateTable(int originalCaseId, int newCaseId, string tableName);
        protected abstract int GetInfOnTreatmentChosenByUserOrCreateNewOneIfNotFound(string userChTreatment);


        protected virtual void CleanUp()
        {
            try
            {

                logFileWriter.Close();
                logFileWriter.Dispose();
                logFileStream.Close();
                logFileStream.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}
