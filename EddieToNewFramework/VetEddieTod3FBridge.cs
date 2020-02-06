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
        List<VetEddieEddieCase> cases = new List<VetEddieEddieCase>();

        const string TABLE_NAME = "cases";
        public VetEddieTod3FBridge(string applicationVersion) : base(applicationVersion)
        {
        }

        public override void TransposeContents()
        {
            Console.WriteLine("Starting the copy process");
            CopyCaseData(TABLE_NAME);
        }

        protected override bool CheckIfCaseWasAlreadyInserted(int originalCaseID, string tableName)
        {
            throw new NotImplementedException();
        }

        protected override void CleanUpNewPatientAndNewOwnerAsAResultOfAnError(int patientID, int ownerID)
        {
            throw new NotImplementedException();
        }

        

        protected override void CopyCaseData(string tableName)
        {
            string rawSql = "SELECT veteddie_eddie.cases.*, appDiseaseRank.percentage AS ucDiseasePercentage, " +
                "appDiseaseRank.rank AS ucDiseaseRank FROM veteddie_eddie.cases, disease, " +
                "appDiseaseRank WHERE disease.diseaseID = cases.ucDisease " +
                "AND appDiseaseRank.caseID = cases.caseID " +
                "AND appDiseaseRank.diseaseID = cases.ucDisease " +
                "ORDER BY cases.caseID ASC";
            string[] parameters = { };

            var cases = vetEddieContext.VetEddieCaseQuery.FromSql<VetEddieEddieCase>(rawSql, parameters).ToList();

            List<Cases> copiedCases = cases.Select(c =>
           new Cases { ApplicationVersion = appVersion,
               DateOfCaseLogged = c.Date,
               DateOfCaseObserved = c.Date,
               OriginId = c.CaseId,
               Location = c.Region + ","+c.Location,
                OriginDbname ="veteddie_eddie" }).ToList();

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
            throw new NotImplementedException();
        }

        protected override int IdentifyOrCreateOwnerOfCase(string name, string region, string location)
        {
            throw new NotImplementedException();
        }
    }
}
