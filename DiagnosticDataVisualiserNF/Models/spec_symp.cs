namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.spec_symp")]
    public partial class spec_symp
    {
        public int spec_sympID { get; set; }

        public int speciousID { get; set; }

        public int symptomID { get; set; }
    }
}
