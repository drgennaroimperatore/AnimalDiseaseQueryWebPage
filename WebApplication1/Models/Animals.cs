namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.Animals")]
    public partial class Animals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animals()
        {
            Likelihoods = new HashSet<Likelihoods>();
            Patients = new HashSet<Patients>();
            PriorsDiseases = new HashSet<PriorsDiseases>();
            SignCores = new HashSet<SignCores>();
        }

        public int Id { get; set; }

        [StringLength(1073741823)]
        public string Name { get; set; }

        [StringLength(1073741823)]
        public string Sex { get; set; }

        [StringLength(1073741823)]
        public string Age { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Likelihoods> Likelihoods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patients> Patients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriorsDiseases> PriorsDiseases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignCores> SignCores { get; set; }
    }
}
