namespace DDNF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.Farmer")]
    public partial class Farmer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farmer()
        {
            Herds = new HashSet<Herd>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string firstName { get; set; }

        [Required]
        [StringLength(100)]
        public string secondName { get; set; }

        [Required]
        [StringLength(100)]
        public string region { get; set; }

        [Required]
        [StringLength(100)]
        public string district { get; set; }

        [Required]
        [StringLength(100)]
        public string kebele { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Herd> Herds { get; set; }
    }
}
