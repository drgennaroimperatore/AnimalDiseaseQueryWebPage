namespace DiagnosticDataVisualiser
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestFramework : DbContext
    {
        public TestFramework()
            : base("name=TestFramework")
        {
        }

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
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
                .Map(m => m.ToTable("TreatmentDiseases", "DebugFramework"));

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

            modelBuilder.Entity<SignForCas>()
                .Property(e => e.SignPresence)
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

            modelBuilder.Entity<UserClaim>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserClaim>()
                .Property(e => e.ClaimType)
                .IsUnicode(false);

            modelBuilder.Entity<UserClaim>()
                .Property(e => e.ClaimValue)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.LoginProvider)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.ProviderKey)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SecurityStamp)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);
        }
    }
}
