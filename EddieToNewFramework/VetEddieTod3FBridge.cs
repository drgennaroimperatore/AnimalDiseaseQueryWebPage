using System;
using System.Collections.Generic;
using System.Text;

namespace EddieToNewFramework
{
    class VetEddieTod3FBridge : Bridge
    {

        const string TABLE_NAME = "cases";
        public VetEddieTod3FBridge(string applicationVersion) : base(applicationVersion)
        {
        }

        public override void TransposeContents()
        {
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
            throw new NotImplementedException();
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
