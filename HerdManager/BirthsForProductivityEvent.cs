namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.BirthsForProductivityEvent")]
    public partial class BirthsForProductivityEvent
    {
        public int ID { get; set; }

        public int nOfGestatingAnimals { get; set; }

        public int nOfBirths { get; set; }

        public int productivityEventID { get; set; }

        public virtual ProductivityEvent ProductivityEvent { get; set; }

        public bool HasBeenUpdated (BirthsForProductivityEvent bpe)
        {
            return ((this.ID == bpe.ID
                && this.productivityEventID == bpe.productivityEventID)
                && (this.nOfBirths != bpe.nOfBirths ||
                    this.nOfGestatingAnimals != bpe.nOfGestatingAnimals));
        }
    }
}
