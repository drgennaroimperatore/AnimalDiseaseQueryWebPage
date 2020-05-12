namespace DDNF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.SignForCases")]
    public partial class SignForCas
    {
        public int ID { get; set; }

        public int SignID { get; set; }

        public int CaseID { get; set; }

        public string SignPresence { get; set; }

        public virtual Case Case { get; set; }

        public virtual Sign Sign { get; set; }
    }
}
