using AirlineApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Persistence;

/// <summary>
/// Represents the database context for the Airline application.
/// Configures entities, relationships, and constraints.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AircraftFamily> AircraftFamilies { get; set; }
    public DbSet<AircraftModel> AircraftModels { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AircraftFamily>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Manufacturer).IsRequired().HasMaxLength(128);
            b.HasIndex(x => new { x.Name, x.Manufacturer }).IsUnique();
        });

        modelBuilder.Entity<AircraftModel>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.Property(x => x.Name).IsRequired().HasMaxLength(64);
            b.Property(x => x.FlightRange).IsRequired();
            b.Property(x => x.PassengerCapacity).IsRequired();
            b.Property(x => x.CargoCapacity).IsRequired();

            b.HasOne(x => x.Family)
                .WithMany()
                .HasForeignKey("FamilyId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Flight>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.Property(x => x.Code).IsRequired().HasMaxLength(16);
            b.Property(x => x.Departure).IsRequired().HasMaxLength(64);
            b.Property(x => x.Arrival).IsRequired().HasMaxLength(64);
            b.Property(x => x.Duration);
            b.Property(x => x.DepartureDateTime);
            b.Property(x => x.ArrivalDateTime);

            b.HasIndex(x => x.Code).IsUnique();

            b.HasOne(x => x.AircraftModel)
                .WithMany()
                .HasForeignKey("AircraftModelId")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Passenger>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.Property(x => x.PassportNumber).IsRequired().HasMaxLength(32);
            b.Property(x => x.FullName).IsRequired().HasMaxLength(128);
            b.HasIndex(x => x.PassportNumber).IsUnique();
        });

        modelBuilder.Entity<Ticket>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.Property(x => x.SeatNumber).IsRequired().HasMaxLength(8);
            b.Property(x => x.HasHandLuggage).IsRequired();

            b.HasOne(x => x.Flight)
                .WithMany()
                .HasForeignKey("FlightId")
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Passenger)
                .WithMany()
                .HasForeignKey("PassengerId")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
