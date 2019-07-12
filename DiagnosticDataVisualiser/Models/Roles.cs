namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eddie.Roles")]
    public partial class Roles
    {
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
