using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EddieToNewFramework
{
    public partial class TestFrameworkContext : DbContext
    {
        public TestFrameworkContext()
        {
        }

        public TestFrameworkContext(DbContextOptions<TestFrameworkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Cases> Cases { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Likelihoods> Likelihoods { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<PriorsDiseases> PriorsDiseases { get; set; }
        public virtual DbSet<ResultForCases> ResultForCases { get; set; }
        public virtual DbSet<SignCores> SignCores { get; set; }
        public virtual DbSet<SignForCases> SignForCases { get; set; }
        public virtual DbSet<Signs> Signs { get; set; }
        public virtual DbSet<SuspectCases> SuspectCases { get; set; }
        public virtual DbSet<TreatmentDiseases> TreatmentDiseases { get; set; }
        public virtual DbSet<Treatments> Treatments { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=188.121.44.186;port=3306;database=TestFramework;uid=gennaro2;password=sha9tTer");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animals>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Age).HasColumnType("longtext");

                entity.Property(e => e.Name).HasColumnType("longtext");

                entity.Property(e => e.Sex).HasColumnType("longtext");
            });

            modelBuilder.Entity<Cases>(entity =>
            {
                entity.HasIndex(e => e.DiseaseChosenByUserId)
                    .HasName("IX_DiseaseChosenByUserID");

                entity.HasIndex(e => e.DiseasePredictedByAppId)
                    .HasName("IX_DiseasePredictedByAppID");

                entity.HasIndex(e => e.PatientId)
                    .HasName("IX_PatientID");

                entity.HasIndex(e => e.TreatmentChosenByUserId)
                    .HasName("IX_TreatmentChosenByUserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApplicationVersion).HasColumnType("longtext");

                entity.Property(e => e.Comments).HasColumnType("longtext");

                entity.Property(e => e.DateOfCaseLogged).HasColumnType("datetime");

                entity.Property(e => e.DateOfCaseObserved).HasColumnType("datetime");

                entity.Property(e => e.DiseaseChosenByUserId)
                    .HasColumnName("DiseaseChosenByUserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseasePredictedByAppId)
                    .HasColumnName("DiseasePredictedByAppID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Location).HasColumnType("longtext");

                entity.Property(e => e.OriginDbname)
                    .HasColumnName("OriginDBName")
                    .HasColumnType("longtext");

                entity.Property(e => e.OriginId)
                    .HasColumnName("OriginID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OriginTableName).HasColumnType("longtext");

                entity.Property(e => e.PatientId)
                    .HasColumnName("PatientID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RankOfDiseaseChosenByUser).HasColumnType("int(11)");

                entity.Property(e => e.TreatmentChosenByUserId)
                    .HasColumnName("TreatmentChosenByUserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.DiseaseChosenByUser)
                    .WithMany(p => p.CasesDiseaseChosenByUser)
                    .HasForeignKey(d => d.DiseaseChosenByUserId);

                entity.HasOne(d => d.DiseasePredictedByApp)
                    .WithMany(p => p.CasesDiseasePredictedByApp)
                    .HasForeignKey(d => d.DiseasePredictedByAppId);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.PatientId);

                entity.HasOne(d => d.TreatmentChosenByUser)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.TreatmentChosenByUserId);
            });

            modelBuilder.Entity<Diseases>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("longtext");
            });

            modelBuilder.Entity<Likelihoods>(entity =>
            {
                entity.HasIndex(e => e.AnimalId)
                    .HasName("IX_AnimalId");

                entity.HasIndex(e => e.DiseaseId)
                    .HasName("IX_DiseaseId");

                entity.HasIndex(e => e.SignId)
                    .HasName("IX_SignId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AnimalId).HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId).HasColumnType("int(11)");

                entity.Property(e => e.SignId).HasColumnType("int(11)");

                entity.Property(e => e.Value).HasColumnType("longtext");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Likelihoods)
                    .HasForeignKey(d => d.AnimalId);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.Likelihoods)
                    .HasForeignKey(d => d.DiseaseId);

                entity.HasOne(d => d.Sign)
                    .WithMany(p => p.Likelihoods)
                    .HasForeignKey(d => d.SignId);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(150)");

                entity.Property(e => e.ContextKey)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<Owners>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("longtext");

                entity.Property(e => e.Profession).HasColumnType("longtext");
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasIndex(e => e.AnimalId)
                    .HasName("IX_AnimalID");

                entity.HasIndex(e => e.OwnerId)
                    .HasName("IX_OwnerID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnimalId)
                    .HasColumnName("AnimalID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OwnerId)
                    .HasColumnName("OwnerID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.AnimalId);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<PriorsDiseases>(entity =>
            {
                entity.HasIndex(e => e.AnimalId)
                    .HasName("IX_AnimalID");

                entity.HasIndex(e => e.DiseaseId)
                    .HasName("IX_DiseaseID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AnimalId)
                    .HasColumnName("AnimalID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("DiseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Probability).HasColumnType("longtext");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.PriorsDiseases)
                    .HasForeignKey(d => d.AnimalId);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.PriorsDiseases)
                    .HasForeignKey(d => d.DiseaseId);
            });

            modelBuilder.Entity<ResultForCases>(entity =>
            {
                entity.HasIndex(e => e.CaseId)
                    .HasName("IX_CaseID");

                entity.HasIndex(e => e.DiseaseId)
                    .HasName("IX_DiseaseID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("CaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("DiseaseID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.ResultForCases)
                    .HasForeignKey(d => d.CaseId);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.ResultForCases)
                    .HasForeignKey(d => d.DiseaseId);
            });

            modelBuilder.Entity<SignCores>(entity =>
            {
                entity.HasIndex(e => e.AnimalId)
                    .HasName("IX_AnimalID");

                entity.HasIndex(e => e.SignId)
                    .HasName("IX_SignID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnimalId)
                    .HasColumnName("AnimalID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SignId)
                    .HasColumnName("SignID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.SignCores)
                    .HasForeignKey(d => d.AnimalId);

                entity.HasOne(d => d.Sign)
                    .WithMany(p => p.SignCores)
                    .HasForeignKey(d => d.SignId);
            });

            modelBuilder.Entity<SignForCases>(entity =>
            {
                entity.HasIndex(e => e.CaseId)
                    .HasName("IX_CaseID");

                entity.HasIndex(e => e.SignId)
                    .HasName("IX_SignID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("CaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SignId)
                    .HasColumnName("SignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SignPresence).HasColumnType("varchar(11)");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.SignForCases)
                    .HasForeignKey(d => d.CaseId);

                entity.HasOne(d => d.Sign)
                    .WithMany(p => p.SignForCases)
                    .HasForeignKey(d => d.SignId);
            });

            modelBuilder.Entity<Signs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("longtext");

                entity.Property(e => e.Probability).HasColumnType("longtext");

                entity.Property(e => e.TypeOfValue)
                    .HasColumnName("Type_of_Value")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SuspectCases>(entity =>
            {
                entity.HasIndex(e => e.DiseaseChosenByUserId)
                    .HasName("IX_DiseaseChosenByUserID");

                entity.HasIndex(e => e.DiseasePredictedByAppId)
                    .HasName("IX_DiseasePredictedByAppID");

                entity.HasIndex(e => e.PatientId)
                    .HasName("IX_PatientID");

                entity.HasIndex(e => e.TreatmentChosenByUserId)
                    .HasName("IX_TreatmentChosenByUserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApplicationVersion).HasColumnType("longtext");

                entity.Property(e => e.Comments).HasColumnType("longtext");

                entity.Property(e => e.DateOfCaseLogged).HasColumnType("datetime");

                entity.Property(e => e.DateOfCaseObserved).HasColumnType("datetime");

                entity.Property(e => e.DiseaseChosenByUserId)
                    .HasColumnName("DiseaseChosenByUserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseasePredictedByAppId)
                    .HasColumnName("DiseasePredictedByAppID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Location).HasColumnType("longtext");

                entity.Property(e => e.OriginDbname)
                    .HasColumnName("OriginDBName")
                    .HasColumnType("longtext");

                entity.Property(e => e.OriginId)
                    .HasColumnName("OriginID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OriginTableName).HasColumnType("longtext");

                entity.Property(e => e.PatientId)
                    .HasColumnName("PatientID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RankOfDiseaseChosenByUser).HasColumnType("int(11)");

                entity.Property(e => e.TreatmentChosenByUserId)
                    .HasColumnName("TreatmentChosenByUserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.DiseaseChosenByUser)
                    .WithMany(p => p.SuspectCasesDiseaseChosenByUser)
                    .HasForeignKey(d => d.DiseaseChosenByUserId);

                entity.HasOne(d => d.DiseasePredictedByApp)
                    .WithMany(p => p.SuspectCasesDiseasePredictedByApp)
                    .HasForeignKey(d => d.DiseasePredictedByAppId);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.SuspectCases)
                    .HasForeignKey(d => d.PatientId);

                entity.HasOne(d => d.TreatmentChosenByUser)
                    .WithMany(p => p.SuspectCases)
                    .HasForeignKey(d => d.TreatmentChosenByUserId);
            });

            modelBuilder.Entity<TreatmentDiseases>(entity =>
            {
                entity.HasKey(e => new { e.TreatmentId, e.DiseaseId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.DiseaseId)
                    .HasName("IX_Disease_Id");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("IX_Treatment_Id");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("Treatment_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("Disease_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.TreatmentDiseases)
                    .HasForeignKey(d => d.DiseaseId);

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TreatmentDiseases)
                    .HasForeignKey(d => d.TreatmentId);
            });

            modelBuilder.Entity<Treatments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Info).HasColumnType("longtext");
            });
        }
    }
}
