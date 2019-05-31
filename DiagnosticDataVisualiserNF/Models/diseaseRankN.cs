namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.diseaseRankN")]
    public partial class diseaseRankN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int caseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int rank { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public string diseaseName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string percentage { get; set; }
    }
}
