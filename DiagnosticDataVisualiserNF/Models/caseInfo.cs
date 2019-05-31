namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.caseInfo")]
    public partial class caseInfo
    {
        [Key]
        public int caseID { get; set; }

        [Required]
        [StringLength(10)]
        public string date { get; set; }

        [Required]
        [StringLength(40)]
        public string owner { get; set; }

        [Required]
        [StringLength(10)]
        public string animalID { get; set; }

        [Required]
        [StringLength(25)]
        public string region { get; set; }

        [Required]
        [StringLength(24)]
        public string location { get; set; }

        [Required]
        [StringLength(20)]
        public string species { get; set; }

        [Required]
        [StringLength(40)]
        public string age { get; set; }

        [Required]
        [StringLength(10)]
        public string breed { get; set; }

        [Required]
        [StringLength(10)]
        public string sex { get; set; }

        [Required]
        [StringLength(40)]
        public string userCHdisease { get; set; }

        [Required]
        [StringLength(500)]
        public string userCHtreatment { get; set; }

        [Required]
        [StringLength(1000)]
        public string comment { get; set; }

        [Required]
        [StringLength(200)]
        public string commentTreatment { get; set; }
    }
}
