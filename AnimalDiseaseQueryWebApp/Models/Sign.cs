//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnimalDiseaseQueryWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sign()
        {
            this.Probabilities = new HashSet<Probability>();
        }
    
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string Type_of_Value { get; set; }
        public string Value { get; set; }
        public string Probability { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Probability> Probabilities { get; set; }
        public virtual Animal Animal { get; set; }
    }
}