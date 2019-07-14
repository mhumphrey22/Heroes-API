using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HeroesAPI.Models
{
    public partial class TourOfHeroesContext : DbContext
    {
        public TourOfHeroesContext()
        {
        }

        public TourOfHeroesContext(DbContextOptions<TourOfHeroesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hero> Hero { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TourOfHeroes;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Hero__737584F68D1B1948")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
