namespace EvFleetOptimizer.API.DTOs;

public class PublicChargerDetourDto
{
    public int ChargerId { get; set; }
    public string Location { get; set; } = string.Empty;
    public double DistanceFromRouteKm { get; set; }
    public double EstimatedWaitMinutes { get; set; }
    public double PricePerKwh { get; set; }
}
