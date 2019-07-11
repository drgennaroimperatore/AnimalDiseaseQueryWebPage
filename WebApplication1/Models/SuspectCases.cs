namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.SuspectCases")]
    public partial class SuspectCases
    {
        public int ID { get; set; }

        public int PatientID { get; set; }

        public DateTime DateOfCaseObserved { get; set; }

        public DateTime DateOfCaseLogged { get; set; }

        [StringLength(1073741823)]
        public string Location { get; set; }

        public int DiseaseChosenByUserID { get; set; }

        public int RankOfDiseaseChosenByUser { get; set; }

        public float LikelihoodOfDiseaseChosenByUser { get; set; }

        public int DiseasePredictedByAppID { get; set; }

        public float LikelihoodOfDiseasePredictedByApp { get; set; }

        public int TreatmentChosenByUserID { get; set; }

        [StringLength(1073741823)]
        public string Comments { get; set; }

        [StringLength(1073741823)]
        public string OriginDBName { get; set; }

        [StringLength(1073741823)]
        public string OriginTableName { get; set; }

        public int OriginID { get; set; }

        [StringLength(1073741823)]
        public string ApplicationVersion { get; set; }

        public virtual Diseases Diseases { get; set; }

        public virtual Diseases Diseases1 { get; set; }

        public virtual Patients Patients { get; set; }

        public virtual Treatments Treatments { get; set; }
    }
}
