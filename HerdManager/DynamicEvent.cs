namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.DynamicEvent")]
    public partial class DynamicEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DynamicEvent()
        {
            AnimalMovementsForDynamicEvents = new HashSet<AnimalMovementsForDynamicEvent>();
            DeathsForDynamicEvents = new HashSet<DeathsForDynamicEvent>();
        }

        public int ID { get; set; }

        public int herdVisitID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalMovementsForDynamicEvent> AnimalMovementsForDynamicEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeathsForDynamicEvent> DeathsForDynamicEvents { get; set; }

        public virtual HerdVisit HerdVisit { get; set; }
    }
}
