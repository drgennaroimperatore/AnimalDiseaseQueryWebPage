namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.Likelihoods")]
    public partial class Likelihoods
    {
        public int Id { get; set; }

        [StringLength(1073741823)]
        public string Value { get; set; }

        public int AnimalId { get; set; }

        public int SignId { get; set; }

        public int DiseaseId { get; set; }

        public virtual Animals Animals { get; set; }

        public virtual Diseases Diseases { get; set; }

        public virtual Signs Signs { get; set; }
    }
}
