namespace EvFleetOptimizer.Core.Entities;

public class TimeOfUseTariff
{
    public int Id { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime EffectiveTo { get; set; }
    public double PricePerKWh { get; set; } // e.g. 0.20 GBP/kWh
    public string TariffName { get; set; } = string.Empty; // “Night Saver”, “Peak”
}
