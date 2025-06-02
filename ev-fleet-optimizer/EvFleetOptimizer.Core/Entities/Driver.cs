namespace EvFleetOptimizer.Core.Entities;

public class Driver
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // “Jane Smith”
    public string Email { get; set; } = string.Empty; // for notifications
    public string PhoneNumber { get; set; } = string.Empty; // optional
    
    // Navigation
    public ICollection<Vehicle> AssignedVehicles { get; set; } = [];
    public ICollection<Trip> AssignedTrips { get; set; } = [];
}
