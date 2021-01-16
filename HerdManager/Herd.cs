namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.Herd")]
    public partial class Herd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Herd()
        {
            HerdVisit = new HashSet<HerdVisit>();
        }

        public int ID { get; set; }

        public int speciesID { get; set; }

        public int farmerID { get; set; }

        public int nBabies { get; set; }

        public int nYoung { get; set; }

        public int nOld { get; set; }

        public virtual Animals Animals { get; set; }

        public virtual Farmer Farmer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HerdVisit> HerdVisit { get; set; }
    }
}
