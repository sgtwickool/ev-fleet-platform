using System.ComponentModel.DataAnnotations;

namespace EvFleetOptimizer.Core.DTOs;

public class CreateVehicleRequestDto
{
    [Required]
    [StringLength(50)]
    public string Make { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Model { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string LicensePlate { get; set; } = string.Empty;

    [Range(1, 200)]
    public double BatteryCapacityKWh { get; set; }

    [Range(1, 2000)]
    public double MaxRangeKm { get; set; }

    public int? AssignedDriverId { get; set; }
}
