namespace DDNF.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ADDB : DbContext
    {
        public ADDB()
            : base("name=Model1")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Likelihood> Likelihoods { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PriorsDiseas> PriorsDiseases { get; set; }
        public virtual DbSet<ResultForCas> ResultForCases { get; set; }
        public virtual DbSet<SignCore> SignCores { get; set; }
        public virtual DbSet<SignForCas> SignForCases { get; set; }
        public virtual DbSet<Sign> Signs { get; set; }
        public virtual DbSet<SuspectCas> SuspectCases { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C__MigrationHistory>()
                .Property(e => e.MigrationId)
                .IsUnicode(false);

            modelBuilder.Entity<C__MigrationHistory>()
                .Property(e => e.ContextKey)
                .IsUnicode(false);

            modelBuilder.Entity<C__MigrationHistory>()
                .Property(e => e.ProductVersion)
                .IsUnicode(false);

            modelBuilder.Entity<Animal>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Animal>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<Animal>()
                .Property(e => e.Age)
                .IsUnicode(false);

            modelBuilder.Entity<Case>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Case>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Case>()
                .Property(e => e.OriginDBName)
                .IsUnicode(false);

            modelBuilder.Entity<Case>()
                .Property(e => e.OriginTableName)
                .IsUnicode(false);

            modelBuilder.Entity<Case>()
                .Property(e => e.ApplicationVersion)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.Cases)
                .WithRequired(e => e.Disease)
                .HasForeignKey(e => e.DiseaseChosenByUserID);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.Cases1)
                .WithRequired(e => e.Disease1)
                .HasForeignKey(e => e.DiseasePredictedByAppID);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.SuspectCases)
                .WithRequired(e => e.Disease)
                .HasForeignKey(e => e.DiseaseChosenByUserID);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.SuspectCases1)
                .WithRequired(e => e.Disease1)
                .HasForeignKey(e => e.DiseasePredictedByAppID);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.Treatments)
                .WithMany(e => e.Diseases)
                .Map(m => m.ToTable("TreatmentDiseases", "TestFramework"));

            modelBuilder.Entity<Likelihood>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Owner>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Owner>()
                .Property(e => e.Profession)
                .IsUnicode(false);

            modelBuilder.Entity<PriorsDiseas>()
                .Property(e => e.Probability)
                .IsUnicode(false);

            modelBuilder.Entity<Sign>()
                .Property(e => e.Probability)
                .IsUnicode(false);

            modelBuilder.Entity<Sign>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCas>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCas>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCas>()
                .Property(e => e.OriginDBName)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCas>()
                .Property(e => e.OriginTableName)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCas>()
                .Property(e => e.ApplicationVersion)
                .IsUnicode(false);

            modelBuilder.Entity<Treatment>()
                .Property(e => e.Info)
                .IsUnicode(false);

            modelBuilder.Entity<Treatment>()
                .HasMany(e => e.Cases)
                .WithRequired(e => e.Treatment)
                .HasForeignKey(e => e.TreatmentChosenByUserID);

            modelBuilder.Entity<Treatment>()
                .HasMany(e => e.SuspectCases)
                .WithRequired(e => e.Treatment)
                .HasForeignKey(e => e.TreatmentChosenByUserID);
        }
    }
}
