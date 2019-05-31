namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.treatment")]
    public partial class treatment
    {
        public int treatmentID { get; set; }

        public int dis_specID { get; set; }

        public int drugID { get; set; }
    }
}
