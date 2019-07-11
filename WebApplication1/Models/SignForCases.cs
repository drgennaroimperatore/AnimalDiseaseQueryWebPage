namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.SignForCases")]
    public partial class SignForCases
    {
        public int ID { get; set; }

        public int SignID { get; set; }

        public int CaseID { get; set; }

        public int SignPresence { get; set; }

        public virtual Cases Cases { get; set; }

        public virtual Signs Signs { get; set; }
    }
}
