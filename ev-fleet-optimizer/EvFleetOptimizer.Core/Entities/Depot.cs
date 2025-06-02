namespace EvFleetOptimizer.Core.Entities;

public class Depot
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // “London Main Depot”
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public int ChargerCount { get; set; } // e.g. 3 chargers available
    public double MaxPowerKW { get; set; } // combined max capacity
    public ICollection<ChargingSession> ChargingSessions { get; set; } = [];
}

