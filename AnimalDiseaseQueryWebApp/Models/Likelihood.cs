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
    
    public partial class Likelihood
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int AnimalId { get; set; }
        public int SignId { get; set; }
        public int DiseaseId { get; set; }
    
        public virtual Animal Animal { get; set; }
        public virtual Sign Sign { get; set; }
        public virtual Disease Disease { get; set; }
    }
}