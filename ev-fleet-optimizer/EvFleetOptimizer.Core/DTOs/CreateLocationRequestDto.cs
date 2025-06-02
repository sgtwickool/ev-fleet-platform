using System.ComponentModel.DataAnnotations;

namespace EvFleetOptimizer.Core.DTOs;

public class CreateLocationRequestDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }

    [StringLength(200)]
    public string AddressLine { get; set; } = string.Empty;
}
