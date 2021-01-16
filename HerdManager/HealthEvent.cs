namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.HealthEvent")]
    public partial class HealthEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HealthEvent()
        {
            BodyConditionForHealthEvent = new HashSet<BodyConditionForHealthEvent>();
            DiseasesForHealthEvent = new HashSet<DiseasesForHealthEvent>();
            SignsForHealthEvent = new HashSet<SignsForHealthEvent>();
        }

        public int ID { get; set; }

        public int herdVisitID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BodyConditionForHealthEvent> BodyConditionForHealthEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseasesForHealthEvent> DiseasesForHealthEvent { get; set; }

        public virtual HerdVisit HerdVisit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignsForHealthEvent> SignsForHealthEvent { get; set; }
    }
}
