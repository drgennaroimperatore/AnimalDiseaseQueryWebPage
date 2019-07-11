namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.__MigrationHistory")]
    public partial class C__MigrationHistory
    {
        [Key]
        [StringLength(150)]
        public string MigrationId { get; set; }

        [Required]
        [StringLength(300)]
        public string ContextKey { get; set; }

        [Required]
        public byte[] Model { get; set; }

        [Required]
        [StringLength(32)]
        public string ProductVersion { get; set; }
    }
}
