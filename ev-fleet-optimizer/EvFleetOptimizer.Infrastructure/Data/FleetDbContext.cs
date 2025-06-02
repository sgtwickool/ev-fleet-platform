using Microsoft.EntityFrameworkCore;
using EvFleetOptimizer.Core.Entities;

namespace EvFleetOptimizer.Infrastructure.Data;

public class FleetDbContext(DbContextOptions<FleetDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Depot> Depots { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<ChargingSession> ChargingSessions { get; set; }
    public DbSet<DepotScheduleEntry> DepotScheduleEntries { get; set; }
    public DbSet<PublicCharger> PublicChargers { get; set; }
    public DbSet<PublicChargerPriceHistory> PublicChargerPriceHistories { get; set; }
    public DbSet<TimeOfUseTariff> TimeOfUseTariffs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Trip <-> Location (Origin)
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.OriginLocation)
            .WithMany(l => l.TripsAsOrigin)
            .HasForeignKey(t => t.OriginLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Trip <-> Location (Destination)
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.DestinationLocation)
            .WithMany(l => l.TripsAsDestination)
            .HasForeignKey(t => t.DestinationLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
