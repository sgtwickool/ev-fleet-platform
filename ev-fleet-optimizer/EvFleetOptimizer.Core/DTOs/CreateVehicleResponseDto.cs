namespace EvFleetOptimizer.Core.DTOs;

public class CreateVehicleResponseDto
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public double BatteryCapacityKWh { get; set; }
    public double MaxRangeKm { get; set; }
    public int? AssignedDriverId { get; set; }
}
