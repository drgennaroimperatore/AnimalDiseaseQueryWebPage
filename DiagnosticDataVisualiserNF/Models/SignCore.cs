namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.SignCores")]
    public partial class SignCore
    {
        public int ID { get; set; }

        public int SignID { get; set; }

        public int AnimalID { get; set; }

        public virtual Animal Animal { get; set; }

        public virtual Sign Sign { get; set; }
    }
}
