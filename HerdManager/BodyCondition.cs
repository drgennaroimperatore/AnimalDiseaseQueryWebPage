namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.BodyCondition")]
    public partial class BodyCondition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string label { get; set; }

        [Required]
        [StringLength(40)]
        public string species { get; set; }

        public int stage { get; set; }

        [Required]
        [StringLength(500)]
        public string description { get; set; }
    }
}
