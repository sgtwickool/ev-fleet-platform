using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.Core.DTOs;
using EvFleetOptimizer.Core.Interfaces;
using EvFleetOptimizer.Core.Entities;
using AutoMapper;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController(IFleetRepository _fleetRepository, IMapper _mapper) : ControllerBase
{
    private readonly IFleetRepository _fleetRepository = _fleetRepository;
    private readonly IMapper _mapper = _mapper;

    [HttpPost]
    public async Task<ActionResult<CreateVehicleResponseDto>> CreateVehicle([FromBody] CreateVehicleRequestDto vehicleRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehicle = _mapper.Map<Vehicle>(vehicleRequest);
        await _fleetRepository.AddVehicleAsync(vehicle);
        var response = _mapper.Map<CreateVehicleResponseDto>(vehicle);
        return Ok(response);
    }
}
