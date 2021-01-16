namespace HerdManager
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HDB : DbContext
    {
        public HDB()
            : base("name=HDB")
        {
        }

        public virtual DbSet<AnimalMovementsForDynamicEvent> AnimalMovementsForDynamicEvent { get; set; }
        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<BirthsForProductivityEvent> BirthsForProductivityEvent { get; set; }
        public virtual DbSet<BodyCondition> BodyCondition { get; set; }
        public virtual DbSet<BodyConditionForHealthEvent> BodyConditionForHealthEvent { get; set; }
        public virtual DbSet<DeathsForDynamicEvent> DeathsForDynamicEvent { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<DiseasesForHealthEvent> DiseasesForHealthEvent { get; set; }
        public virtual DbSet<DynamicEvent> DynamicEvent { get; set; }
        public virtual DbSet<Farmer> Farmer { get; set; }
        public virtual DbSet<HealthEvent> HealthEvent { get; set; }
        public virtual DbSet<Herd> Herd { get; set; }
        public virtual DbSet<HerdVisit> HerdVisit { get; set; }
        public virtual DbSet<MilkForProductivityEvent> MilkForProductivityEvent { get; set; }
        public virtual DbSet<ProductivityEvent> ProductivityEvent { get; set; }
        public virtual DbSet<Signs> Signs { get; set; }
        public virtual DbSet<SignsForHealthEvent> SignsForHealthEvent { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
                .HasMany(e => e.Herd)
                .WithRequired(e => e.Animals)
                .HasForeignKey(e => e.speciesID);

            modelBuilder.Entity<BodyCondition>()
                .Property(e => e.label)
                .IsUnicode(false);

            modelBuilder.Entity<BodyCondition>()
                .Property(e => e.species)
                .IsUnicode(false);

            modelBuilder.Entity<BodyCondition>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<DeathsForDynamicEvent>()
                .Property(e => e.causeOfDeath)
                .IsUnicode(false);

            modelBuilder.Entity<Diseases>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.DiseasesForHealthEvent)
                .WithRequired(e => e.Diseases)
                .HasForeignKey(e => e.diseaseID);

            modelBuilder.Entity<Farmer>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<Farmer>()
                .Property(e => e.secondName)
                .IsUnicode(false);

            modelBuilder.Entity<Farmer>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<Farmer>()
                .Property(e => e.district)
                .IsUnicode(false);

            modelBuilder.Entity<Farmer>()
                .Property(e => e.kebele)
                .IsUnicode(false);

            modelBuilder.Entity<HerdVisit>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<Signs>()
                .Property(e => e.Probability)
                .IsUnicode(false);

            modelBuilder.Entity<Signs>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Signs>()
                .HasMany(e => e.SignsForHealthEvent)
                .WithRequired(e => e.Signs)
                .HasForeignKey(e => e.signID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UUID)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Farmer)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID);
        }
    }
}
