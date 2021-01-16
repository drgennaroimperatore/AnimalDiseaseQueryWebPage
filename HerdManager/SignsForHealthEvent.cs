namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.SignsForHealthEvent")]
    public partial class SignsForHealthEvent
    {
        public int ID { get; set; }

        public int numberOfAffectedBabies { get; set; }

        public int numberOfAffectedYoung { get; set; }

        public int numberOfAffectedOld { get; set; }

        public int healthEventID { get; set; }

        public int signID { get; set; }

        public virtual HealthEvent HealthEvent { get; set; }

        public virtual Signs Signs { get; set; }
    }
}
