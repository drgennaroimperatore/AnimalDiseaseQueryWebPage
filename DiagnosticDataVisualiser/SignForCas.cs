namespace DiagnosticDataVisualiser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DebugFramework.SignForCases")]
    public partial class SignForCas
    {
        public int ID { get; set; }

        public int SignID { get; set; }

        public int CaseID { get; set; }

        [StringLength(1073741823)]
        public string SignPresence { get; set; }

        public virtual Case Case { get; set; }

        public virtual Sign Sign { get; set; }
    }
}
