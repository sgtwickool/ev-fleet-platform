using EvFleetOptimizer.Core.Entities;

namespace EvFleetOptimizer.Core.Interfaces;

public interface IFleetRepository
{
    Task AddVehicleAsync(Vehicle vehicle);
    Task UpdateVehicleAsync(Vehicle vehicle);
    Task RemoveVehicleAsync(int vehicleId);
    Task<Vehicle?> GetVehicleByIdAsync(int vehicleId);
    Task<List<Vehicle>> GetAllVehiclesAsync();

    Task AddDriverAsync(Driver driver);
    Task UpdateDriverAsync(Driver driver);
    Task RemoveDriverAsync(int driverId);
    Task<Driver?> GetDriverByIdAsync(int driverId);
    Task<List<Driver>> GetAllDriversAsync();

    Task AddTripAsync(Trip trip);
    Task UpdateTripAsync(Trip trip);
    Task RemoveTripAsync(int tripId);
    Task<Trip?> GetTripByIdAsync(int tripId);
    Task<List<Trip>> GetAllTripsAsync();

    Task<Location?> GetLocationByIdAsync(int locationId);
    Task AddLocationAsync(Location location);
}
