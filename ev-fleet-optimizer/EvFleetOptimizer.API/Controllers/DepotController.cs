using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.API.DTOs;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/depot")]
public class DepotController : ControllerBase
{
    [HttpGet("schedule")]
    public ActionResult<List<DepotScheduleEntryDto>> GetSchedule([FromQuery] int depotId, [FromQuery] DateOnly date)
    {
        // TODO: Return charger calendar for depot/date
        return Ok(new List<DepotScheduleEntryDto>()); // Placeholder
    }

    [HttpPost("schedule")]
    public ActionResult<DepotScheduleEntryDto> ReserveSlot([FromBody] ReserveSlotRequestDto reservationRequest)
    {
        // TODO: Create DepotScheduleEntry
        return Ok(new DepotScheduleEntryDto()); // Placeholder
    }
}
