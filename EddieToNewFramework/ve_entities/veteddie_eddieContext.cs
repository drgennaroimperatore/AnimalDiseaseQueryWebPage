using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EddieToNewFramework.ve_entities
{
    public partial class veteddie_eddieContext : DbContext
    {
        public veteddie_eddieContext()
        {
        }

        public veteddie_eddieContext(DbContextOptions<veteddie_eddieContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddSymptomsSelected> AddSymptomsSelected { get; set; }
        public virtual DbSet<AppDiseaseRank> AppDiseaseRank { get; set; }
        public virtual DbSet<Cases> Cases { get; set; }
        public virtual DbSet<Disease> Disease { get; set; }
        public virtual DbSet<Drug> Drug { get; set; }
        public virtual DbSet<Imei> Imei { get; set; }
        public virtual DbSet<Probabilities> Probabilities { get; set; }
        public virtual DbSet<SpecDisMain> SpecDisMain { get; set; }
        public virtual DbSet<SpecDisOther> SpecDisOther { get; set; }
        public virtual DbSet<SpecSympAdd> SpecSympAdd { get; set; }
        public virtual DbSet<SpecSympMain> SpecSympMain { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Symptoms> Symptoms { get; set; }
        public virtual DbSet<SymptomsSelected> SymptomsSelected { get; set; }
        public virtual DbSet<Treatment> Treatment { get; set; }
        public virtual DbSet<TreatmentOther> TreatmentOther { get; set; }
        public virtual DbSet<TreatmentsSelected> TreatmentsSelected { get; set; }
        public virtual DbSet<TreatmentsSelectedOther> TreatmentsSelectedOther { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbQuery<VetEddieEddieCase> VetEddieCaseQuery { get; set; }
        public virtual DbQuery<VetEddieSymptoms> VetEddieSymptomsQuery { get; set; }
        public virtual DbQuery<VetEddieResults> VetEddieResultsQuery { get; set; }
        public virtual DbQuery<VetEddieCasesWithSymptoms> VetEddieCasesWithSymptomsQuery { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=188.121.44.186 ;port=3306;database=veteddie_eddie; uid=veteddie_gennaro; password=gennaroimperatore;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddSymptomsSelected>(entity =>
            {
                entity.ToTable("addSymptomsSelected");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<AppDiseaseRank>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("PRIMARY");

                entity.ToTable("appDiseaseRank");

                entity.Property(e => e.Pk)
                    .HasColumnName("pk")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("diseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Cases>(entity =>
            {
                entity.HasKey(e => e.CaseId)
                    .HasName("PRIMARY");

                entity.ToTable("cases");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasColumnName("age")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.AnimalId)
                    .IsRequired()
                    .HasColumnName("animalID")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Breed)
                    .IsRequired()
                    .HasColumnName("breed")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.CommentDisease)
                    .IsRequired()
                    .HasColumnName("commentDisease")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.CommentTreatment)
                    .IsRequired()
                    .HasColumnName("commentTreatment")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.ImeiId)
                    .IsRequired()
                    .HasColumnName("imeiID")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.LabConfirm)
                    .HasColumnName("labConfirm")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LabResult)
                    .HasColumnName("labResult")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Latt).HasColumnName("latt");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Longt).HasColumnName("longt");

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasColumnName("ownerName")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasColumnName("region")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Species)
                    .IsRequired()
                    .HasColumnName("species")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.UcDisease)
                    .HasColumnName("ucDisease")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UcDiseaseOther)
                    .HasColumnName("ucDiseaseOther")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.ToTable("disease");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("diseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseName)
                    .IsRequired()
                    .HasColumnName("diseaseName")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable("drug");

                entity.Property(e => e.DrugId)
                    .HasColumnName("drugID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Advice)
                    .IsRequired()
                    .HasColumnName("advice")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.Dose)
                    .IsRequired()
                    .HasColumnName("dose")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DrugName)
                    .IsRequired()
                    .HasColumnName("drugName")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasColumnName("duration")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RootAdm)
                    .IsRequired()
                    .HasColumnName("rootAdm")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Imei>(entity =>
            {
                entity.ToTable("imei");

                entity.Property(e => e.ImeiId)
                    .HasColumnName("imeiID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.ImeiNumber)
                    .IsRequired()
                    .HasColumnName("imeiNumber")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");

                entity.Property(e => e.Site)
                    .IsRequired()
                    .HasColumnName("site")
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Probabilities>(entity =>
            {
                entity.HasKey(e => e.ProbabilityId)
                    .HasName("PRIMARY");

                entity.ToTable("probabilities");

                entity.Property(e => e.ProbabilityId)
                    .HasColumnName("probabilityID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Probability).HasColumnName("probability");

                entity.Property(e => e.SpecDisMainId)
                    .HasColumnName("spec_disMainID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SpecDisMain>(entity =>
            {
                entity.ToTable("spec_disMain");

                entity.Property(e => e.SpecDisMainId)
                    .HasColumnName("spec_disMainID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("diseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciesId)
                    .HasColumnName("speciesID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SpecDisOther>(entity =>
            {
                entity.ToTable("spec_disOther");

                entity.Property(e => e.SpecDisOtherId)
                    .HasColumnName("spec_disOtherID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("diseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciesId)
                    .HasColumnName("speciesID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SpecSympAdd>(entity =>
            {
                entity.ToTable("spec_sympAdd");

                entity.Property(e => e.SpecSympAddId)
                    .HasColumnName("spec_sympAddID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciesId)
                    .HasColumnName("speciesID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SpecSympMain>(entity =>
            {
                entity.ToTable("spec_sympMain");

                entity.Property(e => e.SpecSympMainId)
                    .HasColumnName("spec_sympMainID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciesId)
                    .HasColumnName("speciesID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.ToTable("species");

                entity.Property(e => e.SpeciesId)
                    .HasColumnName("speciesID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciesName)
                    .IsRequired()
                    .HasColumnName("speciesName")
                    .HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<Symptoms>(entity =>
            {
                entity.HasKey(e => e.SymptomId)
                    .HasName("PRIMARY");

                entity.ToTable("symptoms");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SymptomName)
                    .IsRequired()
                    .HasColumnName("symptomName")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<SymptomsSelected>(entity =>
            {
                entity.ToTable("symptomsSelected");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Selection)
                    .IsRequired()
                    .HasColumnName("selection")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.ToTable("treatment");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("treatmentID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DrugId)
                    .HasColumnName("drugID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpecDisMainId)
                    .HasColumnName("spec_disMainID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TreatmentOther>(entity =>
            {
                entity.HasKey(e => e.TreatmentId)
                    .HasName("PRIMARY");

                entity.ToTable("treatmentOther");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("treatmentID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DrugId)
                    .HasColumnName("drugID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpecDisOtherId)
                    .HasColumnName("spec_disOtherID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TreatmentsSelected>(entity =>
            {
                entity.ToTable("treatmentsSelected");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DrugId)
                    .HasColumnName("drugID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TreatmentsSelectedOther>(entity =>
            {
                entity.ToTable("treatmentsSelectedOther");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DrugId)
                    .HasColumnName("drugID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(50)");
            });
        }
    }
}
