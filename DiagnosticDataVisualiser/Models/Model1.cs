namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Eddie : DbContext
    {
        public Eddie()
            : base("name=Model1")
        {
        }

        public virtual DbSet<caseInfo> caseInfoes { get; set; }
        public virtual DbSet<dis_spec> dis_spec { get; set; }
        public virtual DbSet<disease> diseases { get; set; }
        public virtual DbSet<drug> drugs { get; set; }
        public virtual DbSet<IEMI> IEMIs { get; set; }
        public virtual DbSet<ot_dis_spec> ot_dis_spec { get; set; }
        public virtual DbSet<ot_disease> ot_disease { get; set; }
        public virtual DbSet<probablity> probablities { get; set; }
        public virtual DbSet<rank> ranks { get; set; }
        public virtual DbSet<rankTable1> rankTable1 { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<setCase> setCases { get; set; }
        public virtual DbSet<spec_symp> spec_symp { get; set; }
        public virtual DbSet<species> species { get; set; }
        public virtual DbSet<symptom> symptoms { get; set; }
        public virtual DbSet<treatment> treatments { get; set; }
        public virtual DbSet<diseaseRank> diseaseRanks { get; set; }
        public virtual DbSet<diseaseRankN> diseaseRankNs { get; set; }
        public virtual DbSet<selectedSymptom> selectedSymptoms { get; set; }
        public virtual DbSet<selectedSymptomsN> selectedSymptomsNs { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<caseInfo>()
                .Property(e => e.date)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.owner)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.animalID)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.species)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.age)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.breed)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.sex)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.userCHdisease)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.userCHtreatment)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<caseInfo>()
                .Property(e => e.commentTreatment)
                .IsUnicode(false);

            modelBuilder.Entity<disease>()
                .Property(e => e.diseaseName)
                .IsUnicode(false);

            modelBuilder.Entity<drug>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<drug>()
                .Property(e => e.dose)
                .IsUnicode(false);

            modelBuilder.Entity<drug>()
                .Property(e => e.duration)
                .IsUnicode(false);

            modelBuilder.Entity<drug>()
                .Property(e => e.rootAdmin)
                .IsUnicode(false);

            modelBuilder.Entity<drug>()
                .Property(e => e.advice)
                .IsUnicode(false);

            modelBuilder.Entity<IEMI>()
                .Property(e => e.IEMINUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<IEMI>()
                .Property(e => e.FName)
                .IsUnicode(false);

            modelBuilder.Entity<IEMI>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<IEMI>()
                .Property(e => e.PhNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ot_disease>()
                .Property(e => e.diseaseName)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.date)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.owner)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.animalID)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.species)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.age)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.breed)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.sex)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.userCHdisease)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.userCHtreatment)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.commentTreatment)
                .IsUnicode(false);

            modelBuilder.Entity<setCase>()
                .Property(e => e.latLong)
                .IsUnicode(false);

            modelBuilder.Entity<species>()
                .Property(e => e.speciesName)
                .IsUnicode(false);

            modelBuilder.Entity<symptom>()
                .Property(e => e.symptomName)
                .IsUnicode(false);

            
            modelBuilder.Entity<diseaseRank>()
                .Property(e => e.diseaseName)
                .IsUnicode(false);

            modelBuilder.Entity<diseaseRank>()
                .Property(e => e.percentage)
                .IsUnicode(false);

            modelBuilder.Entity<diseaseRankN>()
                .Property(e => e.diseaseName)
                .IsUnicode(false);

            modelBuilder.Entity<diseaseRankN>()
                .Property(e => e.percentage)
                .IsUnicode(false);

            modelBuilder.Entity<selectedSymptom>()
                .Property(e => e.symptomName)
                .IsUnicode(false);

            modelBuilder.Entity<selectedSymptom>()
                .Property(e => e.Selection)
                .IsUnicode(false);

            modelBuilder.Entity<selectedSymptomsN>()
                .Property(e => e.symptomName)
                .IsUnicode(false);

            modelBuilder.Entity<selectedSymptomsN>()
                .Property(e => e.Selection)
                .IsUnicode(false);

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

            modelBuilder.Entity<Roles>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.Name)
                .IsUnicode(false);


        }
    }
}
