namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.UserClaims")]
    public partial class UserClaims
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(1073741823)]
        public string ClaimType { get; set; }

        [StringLength(1073741823)]
        public string ClaimValue { get; set; }
    }
}
