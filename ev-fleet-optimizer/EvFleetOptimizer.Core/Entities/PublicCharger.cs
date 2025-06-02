namespace EvFleetOptimizer.Core.Entities;

public class PublicCharger
{
    public int Id { get; set; }
    public string ProviderName { get; set; } = string.Empty; // “BP Pulse”
    public string StationId { get; set; } = string.Empty; // Provider’s station ID
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public double MaxPowerKW { get; set; } // e.g. 150 kW
    public bool IsFastCharger { get; set; }
    public double CurrentPricePerKWh { get; set; } // real-time price if available
    public bool IsAvailable { get; set; } // from API (true if free)
    public DateTime LastChecked { get; set; } // timestamp of last API poll
    public ICollection<ChargingSession> ChargingSessions { get; set; } = [];
}
