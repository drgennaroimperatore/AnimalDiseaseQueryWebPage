namespace HerdManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerdDatabase.Signs")]
    public partial class Signs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Signs()
        {
            SignsForHealthEvent = new HashSet<SignsForHealthEvent>();
        }

        public int Id { get; set; }

        public int Type_of_Value { get; set; }

        [StringLength(1073741823)]
        public string Probability { get; set; }

        [StringLength(1073741823)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignsForHealthEvent> SignsForHealthEvent { get; set; }
    }
}
