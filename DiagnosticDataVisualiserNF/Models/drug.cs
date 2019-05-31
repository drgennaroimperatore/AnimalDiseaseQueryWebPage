namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.drug")]
    public partial class drug
    {
        public int drugID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string dose { get; set; }

        [Required]
        [StringLength(100)]
        public string duration { get; set; }

        [Required]
        [StringLength(100)]
        public string rootAdmin { get; set; }

        [Required]
        [StringLength(400)]
        public string advice { get; set; }
    }
}
