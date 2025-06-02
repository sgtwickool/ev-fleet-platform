using Microsoft.AspNetCore.Mvc;
using EvFleetOptimizer.Core.DTOs;
using EvFleetOptimizer.Core.Interfaces;
using EvFleetOptimizer.Core.Entities;
using AutoMapper;

namespace EvFleetOptimizer.API.Controllers;

[ApiController]
[Route("api/trips")]
public class TripsController(IFleetRepository _fleetRepository, IMapper _mapper) : ControllerBase
{
    private readonly IFleetRepository _fleetRepository = _fleetRepository;
    private readonly IMapper _mapper = _mapper;

    [HttpPost]
    public async Task<ActionResult<CreateTripResponseDto>> CreateTrip([FromBody] CreateTripRequestDto tripRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Validate driver existence
        if (tripRequest.DriverId == null)
            return BadRequest("DriverId is required.");
        var driver = await _fleetRepository.GetDriverByIdAsync(tripRequest.DriverId.Value);
        if (driver == null)
            return BadRequest("Driver not found.");

        // Validate origin location existence
        var originLocation = await _fleetRepository.GetLocationByIdAsync(tripRequest.OriginLocationId);
        if (originLocation == null)
            return BadRequest($"Origin location with ID {tripRequest.OriginLocationId} not found.");

        // Validate destination location existence
        var destinationLocation = await _fleetRepository.GetLocationByIdAsync(tripRequest.DestinationLocationId);
        if (destinationLocation == null)
            return BadRequest($"Destination location with ID {tripRequest.DestinationLocationId} not found.");

        // Validate vehicle existence if PreferredVehicleId is provided
        if (tripRequest.PreferredVehicleId.HasValue)
        {
            var vehicle = await _fleetRepository.GetVehicleByIdAsync(tripRequest.PreferredVehicleId.Value);
            if (vehicle == null)
                return BadRequest($"Vehicle with ID {tripRequest.PreferredVehicleId.Value} not found.");
        }

        var trip = _mapper.Map<Trip>(tripRequest);
        trip.DriverId = tripRequest.DriverId.Value;
        await _fleetRepository.AddTripAsync(trip);

        var response = new CreateTripResponseDto
        {
            TripId = trip.Id,
            VehicleSuggestions = []
        };
        return Ok(response);
    }
}
