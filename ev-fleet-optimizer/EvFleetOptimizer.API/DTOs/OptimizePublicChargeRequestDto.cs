namespace EvFleetOptimizer.API.DTOs;

public class OptimizePublicChargeRequestDto
{
    public int TripId { get; set; }
    public int VehicleId { get; set; }
    public int OriginLocationId { get; set; }
    public int DestinationLocationId { get; set; }
    public DateTime StartTime { get; set; }
    // Add more trip details as needed
}
