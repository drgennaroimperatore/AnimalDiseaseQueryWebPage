namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.match_results")]
    public partial class match_results
    {
        public int ID { get; set; }

        public int caseID { get; set; }

        [Required]
        [StringLength(50)]
        public string Ed_dis1 { get; set; }

        public float Ed_perc1 { get; set; }

        [Required]
        [StringLength(50)]
        public string Ed_dis2 { get; set; }

        public float Ed_perc2 { get; set; }

        [Required]
        [StringLength(50)]
        public string Ed_dis3 { get; set; }

        public float Ed_perc3 { get; set; }

        [Required]
        [StringLength(50)]
        public string user_dis { get; set; }

        public float user_perc { get; set; }

        
        [StringLength(200)]
        public string comment { get; set; }

        [Required]
        [StringLength(50)]
        public string species { get; set; }

        [Column(TypeName = "date")]
        public DateTime obv_date { get; set; }
    }
}
