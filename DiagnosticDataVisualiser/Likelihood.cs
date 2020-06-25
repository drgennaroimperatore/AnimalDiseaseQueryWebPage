namespace DiagnosticDataVisualiser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DebugFramework.Likelihoods")]
    public partial class Likelihood
    {
        public int Id { get; set; }

        [StringLength(1073741823)]
        public string Value { get; set; }

        public int AnimalId { get; set; }

        public int SignId { get; set; }

        public int DiseaseId { get; set; }

        public virtual Animal Animal { get; set; }

        public virtual Disease Disease { get; set; }

        public virtual Sign Sign { get; set; }
    }
}
