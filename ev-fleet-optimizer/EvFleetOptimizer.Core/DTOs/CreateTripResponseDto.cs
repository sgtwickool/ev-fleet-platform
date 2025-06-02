namespace EvFleetOptimizer.Core.DTOs;

public class CreateTripResponseDto
{
    public int TripId { get; set; }
    public List<VehicleSuggestionDto> VehicleSuggestions { get; set; } = [];
}

public class VehicleSuggestionDto
{
    public int VehicleId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public double EstimatedRange { get; set; }
    public double BatteryLevel { get; set; }
}
