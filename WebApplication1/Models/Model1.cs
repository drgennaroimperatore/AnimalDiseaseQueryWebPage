namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Cases> Cases { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Likelihoods> Likelihoods { get; set; }
        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<PriorsDiseases> PriorsDiseases { get; set; }
        public virtual DbSet<ResultForCases> ResultForCases { get; set; }
        public virtual DbSet<SignCores> SignCores { get; set; }
        public virtual DbSet<SignForCases> SignForCases { get; set; }
        public virtual DbSet<Signs> Signs { get; set; }
        public virtual DbSet<SuspectCases> SuspectCases { get; set; }
        public virtual DbSet<Treatments> Treatments { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

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

            modelBuilder.Entity<Animals>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Animals>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<Animals>()
                .Property(e => e.Age)
                .IsUnicode(false);

            modelBuilder.Entity<Animals>()
                .HasMany(e => e.Likelihoods)
                .WithRequired(e => e.Animals)
                .HasForeignKey(e => e.AnimalId);

            modelBuilder.Entity<Animals>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.Animals)
                .HasForeignKey(e => e.AnimalID);

            modelBuilder.Entity<Animals>()
                .HasMany(e => e.PriorsDiseases)
                .WithRequired(e => e.Animals)
                .HasForeignKey(e => e.AnimalID);

            modelBuilder.Entity<Animals>()
                .HasMany(e => e.SignCores)
                .WithRequired(e => e.Animals)
                .HasForeignKey(e => e.AnimalID);

            modelBuilder.Entity<Cases>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Cases>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Cases>()
                .Property(e => e.OriginDBName)
                .IsUnicode(false);

            modelBuilder.Entity<Cases>()
                .Property(e => e.OriginTableName)
                .IsUnicode(false);

            modelBuilder.Entity<Cases>()
                .Property(e => e.ApplicationVersion)
                .IsUnicode(false);

            modelBuilder.Entity<Cases>()
                .HasMany(e => e.ResultForCases)
                .WithRequired(e => e.Cases)
                .HasForeignKey(e => e.CaseID);

            modelBuilder.Entity<Cases>()
                .HasMany(e => e.SignForCases)
                .WithRequired(e => e.Cases)
                .HasForeignKey(e => e.CaseID);

            modelBuilder.Entity<Diseases>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.Cases)
                .WithRequired(e => e.Diseases)
                .HasForeignKey(e => e.DiseaseChosenByUserID);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.Cases1)
                .WithRequired(e => e.Diseases1)
                .HasForeignKey(e => e.DiseasePredictedByAppID);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.Likelihoods)
                .WithRequired(e => e.Diseases)
                .HasForeignKey(e => e.DiseaseId);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.PriorsDiseases)
                .WithRequired(e => e.Diseases)
                .HasForeignKey(e => e.DiseaseID);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.ResultForCases)
                .WithRequired(e => e.Diseases)
                .HasForeignKey(e => e.DiseaseID);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.SuspectCases)
                .WithRequired(e => e.Diseases)
                .HasForeignKey(e => e.DiseaseChosenByUserID);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.SuspectCases1)
                .WithRequired(e => e.Diseases1)
                .HasForeignKey(e => e.DiseasePredictedByAppID);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.Treatments)
                .WithMany(e => e.Diseases)
                .Map(m => m.ToTable("TreatmentDiseases", "TestFramework").MapLeftKey("Disease_Id").MapRightKey("Treatment_Id"));

            modelBuilder.Entity<Likelihoods>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Owners>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Owners>()
                .Property(e => e.Profession)
                .IsUnicode(false);

            modelBuilder.Entity<Owners>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.Owners)
                .HasForeignKey(e => e.OwnerID);

            modelBuilder.Entity<Patients>()
                .HasMany(e => e.Cases)
                .WithRequired(e => e.Patients)
                .HasForeignKey(e => e.PatientID);

            modelBuilder.Entity<Patients>()
                .HasMany(e => e.SuspectCases)
                .WithRequired(e => e.Patients)
                .HasForeignKey(e => e.PatientID);

            modelBuilder.Entity<PriorsDiseases>()
                .Property(e => e.Probability)
                .IsUnicode(false);

            modelBuilder.Entity<Signs>()
                .Property(e => e.Probability)
                .IsUnicode(false);

            modelBuilder.Entity<Signs>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Signs>()
                .HasMany(e => e.Likelihoods)
                .WithRequired(e => e.Signs)
                .HasForeignKey(e => e.SignId);

            modelBuilder.Entity<Signs>()
                .HasMany(e => e.SignCores)
                .WithRequired(e => e.Signs)
                .HasForeignKey(e => e.SignID);

            modelBuilder.Entity<Signs>()
                .HasMany(e => e.SignForCases)
                .WithRequired(e => e.Signs)
                .HasForeignKey(e => e.SignID);

            modelBuilder.Entity<SuspectCases>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCases>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCases>()
                .Property(e => e.OriginDBName)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCases>()
                .Property(e => e.OriginTableName)
                .IsUnicode(false);

            modelBuilder.Entity<SuspectCases>()
                .Property(e => e.ApplicationVersion)
                .IsUnicode(false);

            modelBuilder.Entity<Treatments>()
                .Property(e => e.Info)
                .IsUnicode(false);

            modelBuilder.Entity<Treatments>()
                .HasMany(e => e.Cases)
                .WithRequired(e => e.Treatments)
                .HasForeignKey(e => e.TreatmentChosenByUserID);

            modelBuilder.Entity<Treatments>()
                .HasMany(e => e.SuspectCases)
                .WithRequired(e => e.Treatments)
                .HasForeignKey(e => e.TreatmentChosenByUserID);

            modelBuilder.Entity<UserClaims>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserClaims>()
                .Property(e => e.ClaimType)
                .IsUnicode(false);

            modelBuilder.Entity<UserClaims>()
                .Property(e => e.ClaimValue)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogins>()
                .Property(e => e.LoginProvider)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogins>()
                .Property(e => e.ProviderKey)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogins>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserRoles>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserRoles>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.SecurityStamp)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserName)
                .IsUnicode(false);
        }
    }
}
