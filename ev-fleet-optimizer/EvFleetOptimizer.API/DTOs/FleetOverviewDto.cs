namespace EvFleetOptimizer.API.DTOs;

public class FleetOverviewDto
{
    public List<VehicleStatusDto> Vehicles { get; set; } = [];
    public List<LowChargeAlertDto> LowChargeAlerts { get; set; } = [];
}

public class VehicleStatusDto
{
    public int VehicleId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public double BatteryLevel { get; set; }
}

public class LowChargeAlertDto
{
    public int VehicleId { get; set; }
    public double BatteryLevel { get; set; }
    public string Message { get; set; } = string.Empty;
}
