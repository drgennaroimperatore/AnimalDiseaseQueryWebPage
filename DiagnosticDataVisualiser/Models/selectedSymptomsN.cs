namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.selectedSymptomsN")]
    public partial class selectedSymptomsN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int caseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string symptomName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string Selection { get; set; }
    }
}
