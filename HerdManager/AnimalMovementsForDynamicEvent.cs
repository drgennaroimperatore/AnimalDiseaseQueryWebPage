namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.AnimalMovementsForDynamicEvent")]
    public partial class AnimalMovementsForDynamicEvent
    {
        public int ID { get; set; }

        public int soldBabies { get; set; }

        public int soldYoung { get; set; }

        public int soldOld { get; set; }

        public int lostBabies { get; set; }

        public int lostYoung { get; set; }

        public int lostOld { get; set; }

        public int boughtBabies { get; set; }

        public int boughtYoung { get; set; }

        public int boughtOld { get; set; }

        public int dynamicEventID { get; set; }

        public virtual DynamicEvent DynamicEvent { get; set; }

        public bool HasBeenUpdated (AnimalMovementsForDynamicEvent amde)
        {
            return (
                (this.ID == amde.ID && this.dynamicEventID == amde.dynamicEventID)
                && 
                ((this.lostBabies != amde.lostBabies || this.lostYoung != amde.lostYoung || this.lostOld != amde.lostOld)
                || (this.boughtBabies != amde.boughtBabies || this.boughtYoung != amde.boughtYoung || this.boughtOld != amde.boughtOld)
                || (this.soldBabies != amde.soldBabies || this.soldYoung != amde.soldYoung || this.soldOld != amde.soldOld))
                );
              
        }
    }
}
