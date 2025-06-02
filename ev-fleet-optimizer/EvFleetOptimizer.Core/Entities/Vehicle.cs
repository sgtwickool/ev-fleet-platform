namespace EvFleetOptimizer.Core.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty; // e.g. “GY21ABC”
    public string Make { get; set; } = string.Empty; // e.g. “Renault”
    public string Model { get; set; } = string.Empty; // e.g. “Zoe”
    public double MaxRangeKm { get; set; } // e.g. 250.0
    public double CurrentSoCPercent { get; set; } // e.g. 60.0
    public bool IsAvailable { get; set; } // true if not on trip/charging
    public double BatteryCapacityKWh { get; set; } // e.g. 52.0

    // Navigation
    public int? AssignedDriverId { get; set; }
    public Driver? AssignedDriver { get; set; }
    public ICollection<Trip> Trips { get; set; } = []; // all past/future trips
    public ICollection<ChargingSession> ChargingSessions { get; set; } = [];
}

