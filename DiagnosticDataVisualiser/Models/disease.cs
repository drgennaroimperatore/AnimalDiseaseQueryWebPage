namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.disease")]
    public partial class disease
    {
        public int diseaseID { get; set; }

        [Required]
        [StringLength(40)]
        public string diseaseName { get; set; }
    }
}
