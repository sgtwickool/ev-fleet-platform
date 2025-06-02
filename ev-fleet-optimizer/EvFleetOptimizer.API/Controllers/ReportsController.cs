using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.API.DTOs;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    [HttpGet("fleet")]
    public ActionResult<FleetReportDto> GetFleetReport([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        // TODO: Return aggregated FleetReport for date range
        return Ok(new FleetReportDto()); // Placeholder
    }
}
