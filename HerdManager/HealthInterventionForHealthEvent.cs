namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.HealthInterventionForHealthEvent")]
    public partial class HealthInterventionForHealthEvent
    {
        public int ID { get; set; }

        public int numberOfAffectedBabies { get; set; }

        public int numberOfAffectedYoung { get; set; }

        public int numberOfAffectedAdult { get; set; }

      
        [StringLength(100)]
        public string vaccination { get; set; }

        
        [StringLength(500)]
        public string comments { get; set; }

        public int healthEventID { get; set; }

        public int healthInterventionID { get; set; }

        public virtual HealthEvent HealthEvent { get; set; }

        public virtual HealthIntervention HealthIntervention { get; set; }
    }
}
