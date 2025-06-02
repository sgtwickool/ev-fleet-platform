using System.ComponentModel.DataAnnotations;

namespace EvFleetOptimizer.Core.DTOs;

public class CreateDriverRequestDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
}
