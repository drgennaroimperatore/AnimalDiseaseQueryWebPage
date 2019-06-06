namespace DiagnosticDataVisualiserNF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.probablity")]
    public partial class probablity
    {
        public int probablityID { get; set; }

        public int dis_specID { get; set; }

        public int symptomID { get; set; }

        [Column("probablity")]
        public float probablity1 { get; set; }
    }
}
