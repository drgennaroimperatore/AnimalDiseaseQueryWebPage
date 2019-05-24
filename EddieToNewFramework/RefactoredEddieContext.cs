using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EddieToNewFramework
{
    public partial class RefactoredEddieContext : DbContext
    {
        public RefactoredEddieContext()
        {
        }

        public RefactoredEddieContext(DbContextOptions<RefactoredEddieContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaseInfo> CaseInfo { get; set; }
        public virtual DbSet<DisSpec> DisSpec { get; set; }
        public virtual DbSet<Disease> Disease { get; set; }
        public virtual DbSet<DiseaseRank> DiseaseRank { get; set; }
        public virtual DbSet<DiseaseRankN> DiseaseRankN { get; set; }
        public virtual DbSet<Drug> Drug { get; set; }
        public virtual DbSet<Iemi> Iemi { get; set; }
        public virtual DbSet<OtDisSpec> OtDisSpec { get; set; }
        public virtual DbSet<OtDisease> OtDisease { get; set; }
        public virtual DbSet<Probablity> Probablity { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<RankTable1> RankTable1 { get; set; }
        public virtual DbSet<SelectedSymptoms> SelectedSymptoms { get; set; }
        public virtual DbSet<SelectedSymptomsN> SelectedSymptomsN { get; set; }
        public virtual DbSet<SetCase> SetCase { get; set; }
        public virtual DbSet<SpecSymp> SpecSymp { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Symptom> Symptom { get; set; }
        public virtual DbSet<Treatment> Treatment { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=188.121.44.186;port=3306;database=RefactoredEddie;uid=gennaro2;password=sha9tTer");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaseInfo>(entity =>
            {
                entity.HasKey(e => e.CaseId)
                    .HasName("PRIMARY");

                entity.ToTable("caseInfo");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasColumnName("age")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.AnimalId)
                    .IsRequired()
                    .HasColumnName("animalID")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Breed)
                    .IsRequired()
                    .HasColumnName("breed")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CommentTreatment)
                    .IsRequired()
                    .HasColumnName("commentTreatment")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("varchar(24)");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("owner")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasColumnName("region")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Species)
                    .IsRequired()
                    .HasColumnName("species")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.UserChdisease)
                    .IsRequired()
                    .HasColumnName("userCHdisease")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.UserChtreatment)
                    .IsRequired()
                    .HasColumnName("userCHtreatment")
                    .HasColumnType("varchar(500)");
            });

            modelBuilder.Entity<DisSpec>(entity =>
            {
                entity.ToTable("dis_spec");

                entity.Property(e => e.DisSpecId)
                    .HasColumnName("dis_specID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("diseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciousId)
                    .HasColumnName("speciousID")
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

            modelBuilder.Entity<DiseaseRank>(entity =>
            {
                entity.ToTable("diseaseRank");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseName)
                    .IsRequired()
                    .HasColumnName("diseaseName")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Percentage)
                    .IsRequired()
                    .HasColumnName("percentage")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<DiseaseRankN>(entity =>
            {
                entity.ToTable("diseaseRankN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseName)
                    .IsRequired()
                    .HasColumnName("diseaseName")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Percentage)
                    .IsRequired()
                    .HasColumnName("percentage")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)");
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
                    .HasColumnType("varchar(400)");

                entity.Property(e => e.Dose)
                    .IsRequired()
                    .HasColumnName("dose")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasColumnName("duration")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RootAdmin)
                    .IsRequired()
                    .HasColumnName("rootAdmin")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Iemi>(entity =>
            {
                entity.ToTable("IEMI");

                entity.Property(e => e.Iemiid)
                    .HasColumnName("IEMIID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Ieminumber)
                    .IsRequired()
                    .HasColumnName("IEMINUMBER")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PhNumber).HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<OtDisSpec>(entity =>
            {
                entity.HasKey(e => e.DisSpecId)
                    .HasName("PRIMARY");

                entity.ToTable("ot_dis_spec");

                entity.Property(e => e.DisSpecId)
                    .HasColumnName("dis_specID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("diseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciousId)
                    .HasColumnName("speciousID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<OtDisease>(entity =>
            {
                entity.HasKey(e => e.DiseaseId)
                    .HasName("PRIMARY");

                entity.ToTable("ot_disease");

                entity.Property(e => e.DiseaseId)
                    .HasColumnName("diseaseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseName)
                    .IsRequired()
                    .HasColumnName("diseaseName")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Probablity>(entity =>
            {
                entity.ToTable("probablity");

                entity.Property(e => e.ProbablityId)
                    .HasColumnName("probablityID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DisSpecId)
                    .HasColumnName("dis_specID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Probablity1).HasColumnName("probablity");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("rank");

                entity.Property(e => e.RankId)
                    .HasColumnName("rankID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId).HasColumnName("diseaseID");

                entity.Property(e => e.OverAll).HasColumnName("overAll");

                entity.Property(e => e.Percentage).HasColumnName("percentage");
            });

            modelBuilder.Entity<RankTable1>(entity =>
            {
                entity.HasKey(e => e.RankId)
                    .HasName("PRIMARY");

                entity.ToTable("rankTable1");

                entity.Property(e => e.RankId)
                    .HasColumnName("rankID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiseaseId).HasColumnName("diseaseID");

                entity.Property(e => e.OverAll).HasColumnName("overAll");

                entity.Property(e => e.Percentage).HasColumnName("percentage");
            });

            modelBuilder.Entity<SelectedSymptoms>(entity =>
            {
                entity.ToTable("selectedSymptoms");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Selection)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.SymptomName)
                    .IsRequired()
                    .HasColumnName("symptomName")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<SelectedSymptomsN>(entity =>
            {
                entity.ToTable("selectedSymptomsN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Selection)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.SymptomName)
                    .IsRequired()
                    .HasColumnName("symptomName")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<SetCase>(entity =>
            {
                entity.HasKey(e => e.CaseId)
                    .HasName("PRIMARY");

                entity.ToTable("setCase");

                entity.Property(e => e.CaseId)
                    .HasColumnName("caseID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasColumnName("age")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.AnimalId)
                    .IsRequired()
                    .HasColumnName("animalID")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Breed)
                    .IsRequired()
                    .HasColumnName("breed")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.CommentTreatment)
                    .IsRequired()
                    .HasColumnName("commentTreatment")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Iemiid)
                    .HasColumnName("IEMIID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LatLong)
                    .IsRequired()
                    .HasColumnName("latLong")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Lconfirm)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("varchar(24)");

                entity.Property(e => e.Lresult)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("owner")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasColumnName("region")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Species)
                    .IsRequired()
                    .HasColumnName("species")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.UserChdisease)
                    .IsRequired()
                    .HasColumnName("userCHdisease")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.UserChtreatment)
                    .IsRequired()
                    .HasColumnName("userCHtreatment")
                    .HasColumnType("varchar(500)");
            });

            modelBuilder.Entity<SpecSymp>(entity =>
            {
                entity.ToTable("spec_symp");

                entity.Property(e => e.SpecSympId)
                    .HasColumnName("spec_sympID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpeciousId)
                    .HasColumnName("speciousID")
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
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Symptom>(entity =>
            {
                entity.ToTable("symptom");

                entity.Property(e => e.SymptomId)
                    .HasColumnName("symptomID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SymptomName)
                    .IsRequired()
                    .HasColumnName("symptomName")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.ToTable("treatment");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("treatmentID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DisSpecId)
                    .HasColumnName("dis_specID")
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
