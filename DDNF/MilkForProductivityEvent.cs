namespace DDNF
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

        public int litresOfMilkPerDay { get; set; }

        public int numberOfLactatingAnimals { get; set; }

        public int productivityEventID { get; set; }

        public virtual ProductivityEvent ProductivityEvent { get; set; }
    }
}
