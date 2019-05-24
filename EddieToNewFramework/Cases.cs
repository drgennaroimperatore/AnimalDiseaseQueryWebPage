using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class Cases
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime DateOfCaseObserved { get; set; }
        public DateTime DateOfCaseLogged { get; set; }
        public string Location { get; set; }
        public int DiseaseChosenByUserId { get; set; }
        public int RankOfDiseaseChosenByUser { get; set; }
        public float LikelihoodOfDiseaseChosenByUser { get; set; }
        public int DiseasePredictedByAppId { get; set; }
        public float LikelihoodOfDiseasePredictedByApp { get; set; }
        public int TreatmentChosenByUserId { get; set; }
        public string Comments { get; set; }
        public string OriginDbname { get; set; }
        public string OriginTableName { get; set; }
        public int OriginId { get; set; }
        public string ApplicationVersion { get; set; }

        public virtual Diseases DiseaseChosenByUser { get; set; }
        public virtual Diseases DiseasePredictedByApp { get; set; }
        public virtual Patients Patient { get; set; }
        public virtual Treatments TreatmentChosenByUser { get; set; }
    }
}
