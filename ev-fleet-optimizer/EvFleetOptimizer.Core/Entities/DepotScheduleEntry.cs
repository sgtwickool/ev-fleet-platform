namespace EvFleetOptimizer.Core.Entities;

public class DepotScheduleEntry
{
    public int Id { get; set; }
    public int DepotId { get; set; }
    public Depot? Depot { get; set; }
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public DateTime ReservedStart { get; set; }
    public DateTime ReservedEnd { get; set; }
    public bool IsConfirmed { get; set; }
}
