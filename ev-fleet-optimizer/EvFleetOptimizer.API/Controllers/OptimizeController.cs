using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.API.DTOs;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/optimize")]
public class OptimizeController : ControllerBase
{
    [HttpPost("publicCharge")]
    public ActionResult<List<PublicChargerDetourDto>> OptimizePublicCharge([FromBody] OptimizePublicChargeRequestDto tripDetails)
    {
        // TODO: Implement logic to return best public charger detours for given trip
        return Ok(new List<PublicChargerDetourDto>()); // Placeholder
    }
}
