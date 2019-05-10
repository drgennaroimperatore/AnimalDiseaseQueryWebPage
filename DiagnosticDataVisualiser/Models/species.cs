namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.species")]
    public partial class species
    {
        public int speciesID { get; set; }

        [Required]
        [StringLength(40)]
        public string speciesName { get; set; }
    }
}
