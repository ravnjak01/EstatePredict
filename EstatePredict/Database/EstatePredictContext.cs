using EstatePredict.Entities;
using EstatePredict.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EstatePredict.Api.Database
{
    public class EstatePredictContext :DbContext
    {
        public EstatePredictContext(DbContextOptions<EstatePredictContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<MarketData> MarketData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Location)
                .WithMany(l => l.Properties)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.PropertyType)
                .WithMany(pt => pt.Properties)
                .HasForeignKey(p => p.PropertyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prediction>()
                .HasOne(p => p.User)
                .WithMany(u => u.Predictions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prediction>()
                .HasOne(p => p.Property)
                .WithMany(p => p.Predictions)
                .HasForeignKey(p => p.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MarketData>()
                .HasOne(m => m.Location)
                .WithMany(l => l.MarketData)
                .HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MarketData>()
                .HasOne(m => m.PropertyType)
                .WithMany(pt => pt.MarketData)
                .HasForeignKey(m => m.PropertyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MarketData>()
                .HasIndex(m => new
                {
                    m.LocationId,
                    m.PropertyTypeId,
                    m.Year
                })
                .IsUnique();

            modelBuilder.Entity<Prediction>()
                .Property(p => p.PredictedPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Prediction>()
                .Property(p => p.PredictedPricePerSquareMeter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MarketData>()
                .Property(m => m.AveragePricePerSquareMeter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MarketData>()
                .Property(m => m.AveragePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Property>()
                .Property(p => p.Area)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Property>()
                .Property(p => p.CurrentPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}