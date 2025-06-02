using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.Core.DTOs;
using EvFleetOptimizer.Core.Interfaces;
using EvFleetOptimizer.Core.Entities;
using AutoMapper;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriversController(IFleetRepository _fleetRepository, IMapper _mapper) : ControllerBase
{
    private readonly IFleetRepository _fleetRepository = _fleetRepository;
    private readonly IMapper _mapper = _mapper;

    [HttpPost]
    public async Task<ActionResult<CreateDriverResponseDto>> CreateDriver([FromBody] CreateDriverRequestDto driverRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var driver = _mapper.Map<Driver>(driverRequest);
        await _fleetRepository.AddDriverAsync(driver);
        var response = _mapper.Map<CreateDriverResponseDto>(driver);
        return Ok(response);
    }
}
