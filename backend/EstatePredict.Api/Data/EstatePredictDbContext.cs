using EstatePredict.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstatePredict.Api.Data;

public class EstatePredictContext : DbContext
{
    public EstatePredictContext(DbContextOptions<EstatePredictContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
    public DbSet<Prediction> Predictions => Set<Prediction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // USER

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.PasswordHash)
                .IsRequired();

            entity.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(u => u.Email)
                .IsUnique();
        });


        // LOCATION

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.Country)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(l => l.City)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(l => l.Municipality)
                .IsRequired()
                .HasMaxLength(100);
        });


        // PROPERTY TYPE

        modelBuilder.Entity<PropertyType>(entity =>
        {
            entity.HasKey(pt => pt.Id);

            entity.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(pt => pt.Name)
                .IsUnique();
        });


        // PROPERTY

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Description)
                .HasMaxLength(2000);

            entity.Property(p => p.Area)
                .HasPrecision(10, 2);

            entity.Property(p => p.CurrentPrice)
                .HasPrecision(18, 2);

            entity.Property(p => p.Condition)
                .IsRequired()
                .HasMaxLength(50);

            // User -> Properties
            entity.HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Location -> Properties
            entity.HasOne(p => p.Location)
                .WithMany(l => l.Properties)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // PropertyType -> Properties
            entity.HasOne(p => p.PropertyType)
                .WithMany(pt => pt.Properties)
                .HasForeignKey(p => p.PropertyTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // PREDICTION

        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.PredictedPrice)
                .HasPrecision(18, 2);

            entity.Property(p => p.PredictedPricePerSquareMeter)
                .HasPrecision(18, 2);

            entity.Property(p => p.ConfidenceScore)
                .HasPrecision(5, 2);

            entity.Property(p => p.ModelVersion)
                .IsRequired()
                .HasMaxLength(100);

            // User -> Predictions
            entity.HasOne(p => p.User)
                .WithMany(u => u.Predictions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Property -> Predictions
            entity.HasOne(p => p.Property)
                .WithMany(p => p.Predictions)
                .HasForeignKey(p => p.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}