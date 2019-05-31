namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.dis_spec")]
    public partial class dis_spec
    {
        public int dis_specID { get; set; }

        public int diseaseID { get; set; }

        public int speciousID { get; set; }
    }
}
