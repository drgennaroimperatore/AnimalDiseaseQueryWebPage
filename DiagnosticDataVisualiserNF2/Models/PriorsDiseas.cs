namespace DiagnosticDataVisualiserNF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.PriorsDiseases")]
    public partial class PriorsDiseas
    {
        public int Id { get; set; }

        public int DiseaseID { get; set; }

        public int AnimalID { get; set; }

        [StringLength(1073741823)]
        public string Probability { get; set; }

        public virtual Animal Animal { get; set; }

        public virtual Disease Disease { get; set; }
    }
}
