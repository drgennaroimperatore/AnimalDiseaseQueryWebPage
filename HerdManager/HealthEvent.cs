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
            BodyConditionForHealthEvents = new HashSet<BodyConditionForHealthEvent>();
            DiseasesForHealthEvents = new HashSet<DiseasesForHealthEvent>();
            HealthInterventionForHealthEvents = new HashSet<HealthInterventionForHealthEvent>();
            SignsForHealthEvents = new HashSet<SignsForHealthEvent>();
        }

        public int ID { get; set; }

        public int herdVisitID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BodyConditionForHealthEvent> BodyConditionForHealthEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseasesForHealthEvent> DiseasesForHealthEvents { get; set; }

        public virtual HerdVisit HerdVisit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthInterventionForHealthEvent> HealthInterventionForHealthEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignsForHealthEvent> SignsForHealthEvents { get; set; }
    }
}
