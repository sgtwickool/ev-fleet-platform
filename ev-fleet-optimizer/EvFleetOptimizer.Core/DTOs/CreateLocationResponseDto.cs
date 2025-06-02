namespace EvFleetOptimizer.Core.DTOs;

public class CreateLocationResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string AddressLine { get; set; } = string.Empty;
}
