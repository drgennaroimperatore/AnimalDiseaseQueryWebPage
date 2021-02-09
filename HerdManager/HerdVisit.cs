namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.HerdVisit")]
    public partial class HerdVisit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HerdVisit()
        {
            DynamicEvents = new HashSet<DynamicEvent>();
            HealthEvents = new HashSet<HealthEvent>();
            ProductivityEvents = new HashSet<ProductivityEvent>();
        }

        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime HerdVisitDate { get; set; }

        public int babiesAtVisit { get; set; }

        public int youngAtVisit { get; set; }

        public int oldAtVisit { get; set; }

        [StringLength(500)]
        public string comments { get; set; }

        public int HerdID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DynamicEvent> DynamicEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthEvent> HealthEvents { get; set; }

        public virtual Herd Herd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductivityEvent> ProductivityEvents { get; set; }
    }
}
