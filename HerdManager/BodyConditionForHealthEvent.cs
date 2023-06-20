namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.BodyConditionForHealthEvent")]
    public partial class BodyConditionForHealthEvent
    {
        public int ID { get; set; }

        public int healthEventID { get; set; }

        public int bodyConditionID { get; set; }

        public int numberOfAffectedBabies { get; set; }

        public int numberOfAffectedYoung { get; set; }

        public int numberOfAffectedOld { get; set; }

        public virtual BodyCondition BodyCondition { get; set; }

        public virtual HealthEvent HealthEvent { get; set; }

        public bool HasBeenUpdated(BodyConditionForHealthEvent bche)
        {
            return ((bche.ID == this.ID &&
                    bche.bodyConditionID == this.bodyConditionID &&
                    bche.healthEventID == this.healthEventID) 
                    && 
                    (bche.numberOfAffectedBabies!= this.numberOfAffectedBabies 
                    || bche.numberOfAffectedYoung != this.numberOfAffectedYoung
                    || bche.numberOfAffectedOld != this.numberOfAffectedOld));
        }
    }
}
