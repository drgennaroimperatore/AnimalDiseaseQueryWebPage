namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.MilkForProductivityEvent")]
    public partial class MilkForProductivityEvent
    {
        public int ID { get; set; }

        public float litresOfMilkPerDay { get; set; }

        public int numberOfLactatingAnimals { get; set; }

        public int productivityEventID { get; set; }

        public virtual ProductivityEvent ProductivityEvent { get; set; }

        public bool HasBeenUpdate(MilkForProductivityEvent mpe)
        {
            return ((this.ID == mpe.ID && this.productivityEventID == mpe.productivityEventID)
                && (this.litresOfMilkPerDay == mpe.litresOfMilkPerDay
                || this.numberOfLactatingAnimals == mpe.numberOfLactatingAnimals));
        }
    }
}
