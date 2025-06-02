namespace EvFleetOptimizer.Core.Entities;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // “Depot A” or “John’s warehouse”
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string AddressLine { get; set; } = string.Empty; // optional street address
    
    // Navigation
    public ICollection<Trip> TripsAsOrigin { get; set; } = [];
    public ICollection<Trip> TripsAsDestination { get; set; } = [];
    public ICollection<Depot> Depots { get; set; } = []; // if this location is a depot
}
