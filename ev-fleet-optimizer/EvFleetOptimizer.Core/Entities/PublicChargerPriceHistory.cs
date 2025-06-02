namespace EvFleetOptimizer.Core.Entities;

public class PublicChargerPriceHistory
{
    public int Id { get; set; }
    public int PublicChargerId { get; set; }
    public PublicCharger? PublicCharger { get; set; }
    public DateTime Timestamp { get; set; }
    public double PricePerKWh { get; set; }
    public bool IsAvailable { get; set; }
}
