namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.Farmer")]
    public partial class Farmer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string firstName { get; set; }

        [Required]
        [StringLength(100)]
        public string secondName { get; set; }

        [Required]
        [StringLength(100)]
        public string region { get; set; }

        [Required]
        [StringLength(100)]
        public string district { get; set; }

        [Required]
        [StringLength(100)]
        public string kebele { get; set; }

        public int UserID { get; set; }

        public virtual ILRIUser User { get; set; }
    }
}
