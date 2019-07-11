namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.ResultForCases")]
    public partial class ResultForCases
    {
        public int ID { get; set; }

        public int DiseaseID { get; set; }

        public int CaseID { get; set; }

        public float PredictedLikelihoodOfDisease { get; set; }

        public virtual Cases Cases { get; set; }

        public virtual Diseases Diseases { get; set; }
    }
}
