namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.rank")]
    public partial class rank
    {
        public int rankID { get; set; }

        [Column(TypeName = "uint")]
        public long? diseaseID { get; set; }

        public double? percentage { get; set; }

        public double? overAll { get; set; }
    }
}