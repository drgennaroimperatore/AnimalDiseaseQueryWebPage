﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ADDB : DbContext
    {
        public ADDB()
            : base("name=ADDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Sign> Signs { get; set; }
        public virtual DbSet<Priors> Priors { get; set; }
        public virtual DbSet<Probability> Probabilities { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
    }
}