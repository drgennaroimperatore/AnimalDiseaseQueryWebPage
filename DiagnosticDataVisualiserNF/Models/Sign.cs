namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestFramework.Signs")]
    public partial class Sign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sign()
        {
            Likelihoods = new HashSet<Likelihood>();
            SignCores = new HashSet<SignCore>();
            SignForCases = new HashSet<SignForCas>();
        }

        public int Id { get; set; }

        public int Type_of_Value { get; set; }

        [StringLength(1073741823)]
        public string Probability { get; set; }

        [StringLength(1073741823)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Likelihood> Likelihoods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignCore> SignCores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignForCas> SignForCases { get; set; }
    }
}
