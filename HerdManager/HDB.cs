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

        public virtual DbSet<AnimalMovementsForDynamicEvent> AnimalMovementsForDynamicEvents { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<BirthsForProductivityEvent> BirthsForProductivityEvents { get; set; }
        public virtual DbSet<DeathsForDynamicEvent> DeathsForDynamicEvents { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<DynamicEvent> DynamicEvents { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<HealthEvent> HealthEvents { get; set; }
        public virtual DbSet<Herd> Herds { get; set; }
        public virtual DbSet<HerdVisit> HerdVisits { get; set; }
        public virtual DbSet<MilkForProductivityEvent> MilkForProductivityEvents { get; set; }
        public virtual DbSet<ProductivityEvent> ProductivityEvents { get; set; }
        public virtual DbSet<Sign> Signs { get; set; }
        public virtual DbSet<SignsForHealthEvent> SignsForHealthEvents { get; set; }
        public virtual DbSet<DiseasesForHealthEvent> DiseasesForHealthEvents { get; set; }
        public virtual DbSet<User> Users { get; }

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

            modelBuilder.Entity<Animal>()
                .HasMany(e => e.Herds)
                .WithRequired(e => e.Animal)
                .HasForeignKey(e => e.speciesID);

            modelBuilder.Entity<DeathsForDynamicEvent>()
                .Property(e => e.causeOfDeath)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.Name)
                .IsUnicode(false);

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

            modelBuilder.Entity<Sign>()
                .Property(e => e.Probability)
                .IsUnicode(false);

            modelBuilder.Entity<Sign>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sign>()
                .HasMany(e => e.SignsForHealthEvents)
                .WithRequired(e => e.Sign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
               .Property(e => e.Name)
               .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UUID)
                .IsUnicode(false);
        }
    }
}
