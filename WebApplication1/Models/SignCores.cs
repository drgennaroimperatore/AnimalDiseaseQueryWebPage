namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.SignCores")]
    public partial class SignCores
    {
        public int ID { get; set; }

        public int SignID { get; set; }

        public int AnimalID { get; set; }

        public virtual Animals Animals { get; set; }

        public virtual Signs Signs { get; set; }
    }
}
