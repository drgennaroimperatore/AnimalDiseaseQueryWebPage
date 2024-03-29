﻿using EddieToNewFramework.ve_entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EddieToNewFramework
{
    class VetEddieTod3FBridge : Bridge
    {

        ve_entities.veteddie_eddieContext vetEddieContext = new ve_entities.veteddie_eddieContext();
        TestFrameworkContext testFrameworkContext = new TestFrameworkContext();
        List<VetEddieEddieCase> cases = new List<VetEddieEddieCase>();

        const string TABLE_NAME = "cases";
        public VetEddieTod3FBridge(string applicationVersion) : base(applicationVersion)
        {
        }

        public override void TransposeContents()
        {
            Console.WriteLine("Starting the copy process");
            CopyCaseData(TABLE_NAME);

            CleanUp();
        }

        protected override bool CheckIfCaseWasAlreadyInserted(int originalCaseID, string tableName)
        {
            throw new NotImplementedException();
        }

        protected override void CleanUpNewPatientAndNewOwnerAsAResultOfAnError(int patientID, int ownerID)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLatestImportedCase()
        {
            DateTime date = new DateTime();

            try
            {
                var casesAlreadyCopied = testFrameworkContext.Cases.Where(c => c.OriginDbname != "D3FFramework" && c.OriginDbname != "Eddie");
                if (casesAlreadyCopied.Count()==0)
                {
                    date = new DateTime(1990, 1, 1);//dummy date in the past
                }
                else
                    date =  testFrameworkContext.Cases.Where(c => c.OriginDbname != "D3FFramework" && c.OriginDbname!="Eddie").Max(c => c.DateOfCaseObserved);

            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(date);
            return date;
        }

        public List<int> GetOriginalIDOfLatestImportedCases(string tableName)
        {
            List<int> origIDs = new List<int>();
            var casesAlreadyCopied = testFrameworkContext.Cases.Where(c => c.OriginDbname != "D3FFramework" && c.OriginDbname != "Eddie");
            if (casesAlreadyCopied.Count() == 0)
                return origIDs;
            var latestCases = casesAlreadyCopied.Select(  x=> new { x.OriginId, x.DateOfCaseObserved}).Where(x => x.DateOfCaseObserved == GetLatestImportedCase());
            origIDs = latestCases.Select(x => x.OriginId).ToList();
            return origIDs;

        }

       
        

        protected override void CopyCaseData(string tableName)
        {

            DateTime latestImportedCase = GetLatestImportedCase();
            string licString = latestImportedCase.ToString("yyyy-MM-dd");
            

            string rawSql = "SELECT veteddie_eddie.cases.*, " +
                "disease.diseaseName AS ucDiseaseName, " +
                "appDiseaseRank.percentage AS ucDiseasePercentage, " +
                "appDiseaseRank.rank AS ucDiseaseRank " +
                "FROM veteddie_eddie.cases, disease, " +
                "appDiseaseRank WHERE disease.diseaseID = cases.ucDisease " +
                "AND appDiseaseRank.caseID = cases.caseID " +
                "AND appDiseaseRank.diseaseID = cases.ucDisease " +
                "AND cases.date >= @p0 "+
                "ORDER BY cases.caseID ASC";

            string nosympRawSql = "SELECT DISTINCT cases.caseID FROM cases,symptomsSelected WHERE symptomsSelected.caseID = cases.caseID";
            string[] parameters = {licString };

            List<Cases> copiedCases = new List<Cases>();
            List<int> latestIDs = GetOriginalIDOfLatestImportedCases(tableName);


            try
            {              
              cases = vetEddieContext.VetEddieCaseQuery.FromSql<VetEddieEddieCase>(rawSql, parameters).ToList();
                List<VetEddieEddieCase> casesToRemove = cases.Where(x => latestIDs.Contains(x.CaseId)).ToList();
                if(casesToRemove.Count>0)
                    cases.RemoveRange(cases.IndexOf(casesToRemove[0]), casesToRemove.Count());
                List<VetEddieCasesWithSymptoms> safeCases = vetEddieContext.VetEddieCasesWithSymptomsQuery.FromSql<VetEddieCasesWithSymptoms>(nosympRawSql,parameters).ToList();
                List<int> safeids = safeCases.Select(x => x.caseID).ToList();
                casesToRemove = cases.Where(x => !safeids.Contains(x.CaseId)).ToList();
                if(casesToRemove.Count>0)
                    cases.RemoveRange(cases.IndexOf(casesToRemove[0]), casesToRemove.Count());
                copiedCases = cases.Select(c =>
              new Cases
              {
                  LikelihoodOfDiseaseChosenByUser = (float)c.ucDiseasePercentage,
                  RankOfDiseaseChosenByUser = c.ucDiseaseRank,
                  DiseaseChosenByUserId = GetDiseaseID(c.ucDiseaseName),
                  DiseasePredictedByAppId = GetTransposedResultsForCase(c.CaseId).First().DiseaseId,
                  ApplicationVersion = appVersion,
                  DateOfCaseLogged = c.Date,
                  DateOfCaseObserved = c.Date,
                  SignForCases = GetTransposedSymptomsFromVetEddie(c.CaseId),
                  ResultForCases = GetTransposedResultsForCase(c.CaseId),
                  OriginId = c.CaseId,
                  Location = c.Region + "," + c.Location,
                  OriginTableName = TABLE_NAME,
                  OriginDbname = "veteddie_eddie",
                  Comments = c.CommentDisease,

                  PatientId = IdentifyOrCreateNewPatient(GetAnimalIDBasedOnCaseInfo(c.Species, c.Sex, c.Age),
                             IdentifyOrCreateOwnerOfCase(c.OwnerName, c.Region, c.Location)),
                  TreatmentChosenByUserId = 1
               }).ToList();

                if(copiedCases.Count==0)
                {
                    Console.WriteLine("Database is up to date. No new cases to add");
                    return;
                }

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Copy process attempt completed");
                try
                {
                    Console.WriteLine("Saving new Cases");
                    testFrameworkContext.Cases.AddRange(copiedCases);
                    testFrameworkContext.SaveChanges();
                    Console.WriteLine("New cases added {0} ", copiedCases.Count);
                }
                catch (Exception e)
                {
                    Console.WriteLine("There was a problem saving cases");
                    Console.WriteLine(e.Message);
                    if (e.InnerException != null)
                    {
                        Console.WriteLine(e.InnerException.Message);
                        
                    }


                }

                
            }

            Console.WriteLine("Copy case");

            
                
                
                /*(c => new Cases
                 {
                     ApplicationVersion = "1.0",
                     Comments = c.CommentDisease,
                     DateOfCaseLogged = c.Date,
                     DateOfCaseObserved = c.Date,
                     DiseaseChosenByUser = c.u



                 }); */
        }

        protected override int GetInfOnTreatmentChosenByUserOrCreateNewOneIfNotFound(string userChTreatment)
        {
            throw new NotImplementedException();
        }

        protected override ReturnValue GetResultsForCaseAndPopulateTable(int originalCaseId, int newCaseId, string tableName)
        {
            throw new NotImplementedException();
        }

        protected override ReturnValue GetSymptomsForCaseAndPopulateTable(int originalCaseId, int newCaseId, string tableName)
        {
          

            return new ReturnValue { ThereWasAnError = true };

        }

        private List<SignForCases> GetTransposedSymptomsFromVetEddie(int originalCaseID)
        {
            List<VetEddieSymptoms> symptoms = new List<VetEddieSymptoms>();
            List<SignForCases> transposedSymptoms = new List<SignForCases>();

            bool therewasanerror = false;

            string rawSql = "SELECT cases.caseID, symptoms.symptomName, " +
                "symptomsSelected.selection, " +
                "CASE symptomsSelected.selection " +
                "WHEN 'Absent' THEN 'NP' " +
                "WHEN 'Present' THEN 'P' " +
                "WHEN 'Unknown' THEN 'NO' " +
                "WHEN '' THEN '--' " +
                "END AS TransposedSelection " +
                "FROM symptomsSelected, cases, symptoms " +
                "WHERE cases.caseID = symptomsSelected.caseID " +
                "AND cases.caseID = @p0 " +
                "AND symptoms.symptomID = symptomsSelected.symptomID";

            try
            {

                symptoms = vetEddieContext.VetEddieSymptomsQuery.FromSql<VetEddieSymptoms>(rawSql, originalCaseID).ToList();

                transposedSymptoms = symptoms.Select(s =>
                
                   

                    new SignForCases
                    {
                        SignId = GetSignID(s.SymptomName),
                        SignPresence = s.TransposedSelection == null ? "--" : s.TransposedSelection ,
                        CaseId = s.CaseId
                    
                   }).ToList();
            
            }
            catch (Exception e)
            {
               
                Console.WriteLine(e.Message);
            }

            if (transposedSymptoms.Count == 0)
            {
                Console.WriteLine("No symp for case " + originalCaseID);
                Console.WriteLine();
            }

            return transposedSymptoms;

        }




        public List<ResultForCases> GetTransposedResultsForCase(int originalCaseID)
        {
            List<ResultForCases> transposedResults = new List<ResultForCases>();

            List<VetEddieResults> vetEddieResults = new List<VetEddieResults>();

            string rawSql = "SELECT cases.caseID, appDiseaseRank.rank, disease.diseaseName, appDiseaseRank.percentage " +
                "FROM veteddie_eddie.appDiseaseRank,cases,disease " +
                "WHERE cases.caseID =@p0 AND appDiseaseRank.diseaseID = disease.diseaseID AND cases.caseID = appDiseaseRank.caseID " +
                "ORDER BY appDiseaseRank.percentage DESC";

            try
            {
                vetEddieResults = vetEddieContext.VetEddieResultsQuery.FromSql(rawSql, originalCaseID).ToList();
                transposedResults = vetEddieResults.Select
                    (s => new ResultForCases { DiseaseId = GetDiseaseID(s.DiseaseName),
                    PredictedLikelihoodOfDisease = (float) s.Percentage }).ToList();

                if (vetEddieResults.Count == 0)
                    Console.WriteLine("No results for case {0}", originalCaseID);
                if (transposedResults.Count == 0)
                    Console.WriteLine("No results for case {0}", originalCaseID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return transposedResults;

        }

        public float convertToFloat (double f)
        {

            return Convert.ToSingle(f);
        }

        protected override int IdentifyOrCreateOwnerOfCase(string name, string region, string location)
        {
            Owners owner = new Owners();
            owner.Name = name;
            owner.Profession = "Eddie User";

            /* if (ADDB.Owners.Select(x => x.Name).Contains(name))
                return ADDB.Owners.Where(x => x.Equals(name)).First().Id;*/

            ADDB.Owners.Add(owner);
            ADDB.SaveChanges();
            if (ADDB.Owners.Count() == 0)
                Console.WriteLine("No owners found");

            int id = ADDB.Owners.Last().Id;
            
            return id;
        }

        protected override int IdentifyOrCreateNewPatient(int animalID, int ownerID)
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
    }
}
