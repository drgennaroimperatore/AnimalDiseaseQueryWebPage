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
            AnimalMovementsForDynamicEvent = new HashSet<AnimalMovementsForDynamicEvent>();
            DeathsForDynamicEvent = new HashSet<DeathsForDynamicEvent>();
        }

        public int ID { get; set; }

        public int herdVisitID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalMovementsForDynamicEvent> AnimalMovementsForDynamicEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeathsForDynamicEvent> DeathsForDynamicEvent { get; set; }

        public virtual HerdVisit HerdVisit { get; set; }
    }
}
