namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.symptom")]
    public partial class symptom
    {
        public int symptomID { get; set; }

        [Required]
        [StringLength(40)]
        public string symptomName { get; set; }
    }
}
