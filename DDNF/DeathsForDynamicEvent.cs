namespace DDNF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.DeathsForDynamicEvent")]
    public partial class DeathsForDynamicEvent
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string causeOfDeath { get; set; }

        public int deadBabies { get; set; }

        public int deadYoung { get; set; }

        public int deadOld { get; set; }

        public int dynamicEventID { get; set; }

        public virtual DynamicEvent DynamicEvent { get; set; }
    }
}
