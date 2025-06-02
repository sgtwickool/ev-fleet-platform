using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.API.DTOs;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/tariffs")]
public class TariffsController : ControllerBase
{
    [HttpPost]
    public IActionResult UploadTariffs([FromBody] TariffUploadRequestDto request)
    {
        // TODO: Accept CSV upload or ToU schedule
        return Ok(); // Placeholder
    }
}
