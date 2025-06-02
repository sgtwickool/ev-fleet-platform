using EvFleetOptimizer.Core.Entities;
using EvFleetOptimizer.Core.Interfaces;
using EvFleetOptimizer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EvFleetOptimizer.Infrastructure.Repositories;

public class FleetRepository(FleetDbContext _dbContext) : IFleetRepository
{
    private readonly FleetDbContext _dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        await _dbContext.Vehicles.AddAsync(vehicle);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateVehicleAsync(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        _dbContext.Vehicles.Update(vehicle);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveVehicleAsync(int vehicleId)
    {
        var vehicle = await _dbContext.Vehicles.FindAsync(vehicleId) ?? throw new KeyNotFoundException($"Vehicle with ID {vehicleId} not found.");
        _dbContext.Vehicles.Remove(vehicle);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
    {
        return await _dbContext.Vehicles.FindAsync(vehicleId);
    }

    public async Task<List<Vehicle>> GetAllVehiclesAsync()
    {
        return await _dbContext.Vehicles.AsNoTracking().ToListAsync();
    }

    public async Task AddDriverAsync(Driver driver)
    {
        ArgumentNullException.ThrowIfNull(driver);
        await _dbContext.Drivers.AddAsync(driver);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDriverAsync(Driver driver)
    {
        ArgumentNullException.ThrowIfNull(driver);
        _dbContext.Drivers.Update(driver);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveDriverAsync(int driverId)
    {
        var driver = await _dbContext.Drivers.FindAsync(driverId) ?? throw new KeyNotFoundException($"Driver with ID {driverId} not found.");
        _dbContext.Drivers.Remove(driver);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Driver?> GetDriverByIdAsync(int driverId)
    {
        return await _dbContext.Drivers.FindAsync(driverId);
    }

    public async Task<List<Driver>> GetAllDriversAsync()
    {
        return await _dbContext.Drivers.AsNoTracking().ToListAsync();
    }

    public async Task AddTripAsync(Trip trip)
    {
        ArgumentNullException.ThrowIfNull(trip);
        await _dbContext.Trips.AddAsync(trip);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTripAsync(Trip trip)
    {
        ArgumentNullException.ThrowIfNull(trip);
        _dbContext.Trips.Update(trip);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveTripAsync(int tripId)
    {
        var trip = await _dbContext.Trips.FindAsync(tripId) ?? throw new KeyNotFoundException($"Trip with ID {tripId} not found.");
        _dbContext.Trips.Remove(trip);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Trip?> GetTripByIdAsync(int tripId)
    {
        return await _dbContext.Trips.FindAsync(tripId);
    }

    public async Task<List<Trip>> GetAllTripsAsync()
    {
        return await _dbContext.Trips.AsNoTracking().ToListAsync();
    }

    public async Task<Location?> GetLocationByIdAsync(int locationId)
    {
        return await _dbContext.Locations.FindAsync(locationId);
    }

    public async Task<List<Location>> GetAllLocationsAsync()
    {
        return await _dbContext.Locations.AsNoTracking().ToListAsync();
    }

    public async Task AddLocationAsync(Location location)
    {
        ArgumentNullException.ThrowIfNull(location);
        await _dbContext.Locations.AddAsync(location);
        await _dbContext.SaveChangesAsync();
    }
}
