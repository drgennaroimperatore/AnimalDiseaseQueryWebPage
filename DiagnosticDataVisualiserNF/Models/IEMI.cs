namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.IEMI")]
    public partial class IEMI
    {
        public int IEMIID { get; set; }

        [Required]
        [StringLength(30)]
        public string IEMINUMBER { get; set; }

        [Required]
        [StringLength(20)]
        public string FName { get; set; }

        [Required]
        [StringLength(20)]
        public string LName { get; set; }

        [StringLength(15)]
        public string PhNumber { get; set; }
    }
}
