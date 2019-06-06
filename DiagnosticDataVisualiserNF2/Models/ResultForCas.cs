namespace DiagnosticDataVisualiserNF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.ResultForCases")]
    public partial class ResultForCas
    {
        public int ID { get; set; }

        public int DiseaseID { get; set; }

        public int CaseID { get; set; }

        public float PredictedLikelihoodOfDisease { get; set; }

        public virtual Case Case { get; set; }

        public virtual Disease Disease { get; set; }
    }
}
