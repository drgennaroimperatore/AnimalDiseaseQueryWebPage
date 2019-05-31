namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.ot_dis_spec")]
    public partial class ot_dis_spec
    {
        [Key]
        public int dis_specID { get; set; }

        public int diseaseID { get; set; }

        public int speciousID { get; set; }
    }
}
