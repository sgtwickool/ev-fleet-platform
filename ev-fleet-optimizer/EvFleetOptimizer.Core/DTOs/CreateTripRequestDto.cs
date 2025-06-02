using System.ComponentModel.DataAnnotations;

namespace EvFleetOptimizer.Core.DTOs;

public class CreateTripRequestDto
{
    [Required]
    public int OriginLocationId { get; set; }
    [Required]
    public int DestinationLocationId { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    public int? PreferredVehicleId { get; set; }
    [Required]
    public int? DriverId { get; set; }
    // Add more trip parameters as needed
}
