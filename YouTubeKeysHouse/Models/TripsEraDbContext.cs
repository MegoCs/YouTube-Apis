using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YouTubeKeysHouse.Models
{
    public partial class TripsEraDbContext : DbContext
    {
        public virtual DbSet<Usertracker> Usertracker { get; set; }
        public virtual DbSet<Youtubekeys> Youtubekeys { get; set; }

        public TripsEraDbContext(DbContextOptions<TripsEraDbContext> options)
: base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usertracker>(entity =>
            {
                entity.ToTable("USERTRACKER");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__USERTRAC__B15BE12E84E335D1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActualKeysProcessed).HasColumnName("ACTUAL_KEYS_PROCESSED");

                entity.Property(e => e.KeysFrom).HasColumnName("KEYS_FROM");

                entity.Property(e => e.KeysToFetch).HasColumnName("KEYS_TO_FETCH");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Youtubekeys>(entity =>
            {
                entity.ToTable("YOUTUBEKEYS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KeyType)
                    .IsRequired()
                    .HasColumnName("KEY_TYPE")
                    .HasMaxLength(1);

                entity.Property(e => e.TheKey)
                    .IsRequired()
                    .HasColumnName("THE_KEY")
                    .HasMaxLength(75);
            });
        }
    }
}
