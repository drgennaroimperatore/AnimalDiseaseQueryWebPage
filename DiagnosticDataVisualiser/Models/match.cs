namespace DiagnosticDataVisualiser.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class match : DbContext
    {
        public match()
            : base("name=Model1")
        {
        }

        public virtual DbSet<match_results> match_results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<match_results>()
                .Property(e => e.Ed_dis1)
                .IsUnicode(false);

            modelBuilder.Entity<match_results>()
                .Property(e => e.Ed_dis2)
                .IsUnicode(false);

            modelBuilder.Entity<match_results>()
                .Property(e => e.Ed_dis3)
                .IsUnicode(false);

            modelBuilder.Entity<match_results>()
                .Property(e => e.user_dis)
                .IsUnicode(false);

            modelBuilder.Entity<match_results>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<match_results>()
                .Property(e => e.species)
                .IsUnicode(false);
        }
    }
}
