namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.ProductivityEvent")]
    public partial class ProductivityEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductivityEvent()
        {
            BirthsForProductivityEvent = new HashSet<BirthsForProductivityEvent>();
            MilkForProductivityEvent = new HashSet<MilkForProductivityEvent>();
        }

        public int ID { get; set; }

        public int herdVisitID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BirthsForProductivityEvent> BirthsForProductivityEvent { get; set; }

        public virtual HerdVisit HerdVisit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MilkForProductivityEvent> MilkForProductivityEvent { get; set; }
    }
}
