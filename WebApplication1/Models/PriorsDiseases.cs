namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.PriorsDiseases")]
    public partial class PriorsDiseases
    {
        public int Id { get; set; }

        public int DiseaseID { get; set; }

        public int AnimalID { get; set; }

        [StringLength(1073741823)]
        public string Probability { get; set; }

        public virtual Animals Animals { get; set; }

        public virtual Diseases Diseases { get; set; }
    }
}
