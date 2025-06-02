using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.Core.DTOs;
using EvFleetOptimizer.Core.Interfaces;
using EvFleetOptimizer.Core.Entities;
using AutoMapper;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController(IFleetRepository _fleetRepository, IMapper _mapper) : ControllerBase
{
    private readonly IFleetRepository _fleetRepository = _fleetRepository;
    private readonly IMapper _mapper = _mapper;

    [HttpPost]
    public async Task<ActionResult<CreateLocationResponseDto>> CreateLocation([FromBody] CreateLocationRequestDto locationRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var location = _mapper.Map<Location>(locationRequest);
        await _fleetRepository.AddLocationAsync(location);
        var response = _mapper.Map<CreateLocationResponseDto>(location);
        return Ok(response);
    }
}
