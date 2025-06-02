namespace EvFleetOptimizer.API.DTOs;

public class ReserveSlotRequestDto
{
    public int DepotId { get; set; }
    public int ChargerId { get; set; }
    public int VehicleId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
