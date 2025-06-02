namespace EvFleetOptimizer.API.DTOs;

public class FleetReportDto
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public List<VehicleReportDto> Vehicles { get; set; } = [];
    public double TotalDistanceKm { get; set; }
    public double TotalEnergyKwh { get; set; }
    public double TotalCost { get; set; }
}

public class VehicleReportDto
{
    public int VehicleId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public double DistanceKm { get; set; }
    public double EnergyKwh { get; set; }
    public double Cost { get; set; }
}
