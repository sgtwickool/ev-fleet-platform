using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.API.DTOs;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/fleet")]
public class FleetController : ControllerBase
{
    [HttpGet("overview")]
    public ActionResult<FleetOverviewDto> GetOverview()
    {
        // TODO: Inject service and return list of vehicles + statuses + low-charge alerts
        return Ok(new FleetOverviewDto()); // Placeholder
    }
}
