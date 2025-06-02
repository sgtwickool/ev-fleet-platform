using AutoMapper;
using EvFleetOptimizer.Core.DTOs;
using EvFleetOptimizer.Core.Entities;

namespace EvFleetOptimizer.API.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTripRequestDto, Trip>()
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.PreferredVehicleId ?? 0))
            .ForMember(dest => dest.OriginLocationId, opt => opt.MapFrom(src => src.OriginLocationId))
            .ForMember(dest => dest.DestinationLocationId, opt => opt.MapFrom(src => src.DestinationLocationId))
            .ForMember(dest => dest.ScheduledStart, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.ScheduledEnd, opt => opt.Ignore())
            .ForMember(dest => dest.DriverId, opt => opt.Ignore())
            .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
            .ForMember(dest => dest.Driver, opt => opt.Ignore())
            .ForMember(dest => dest.OriginLocation, opt => opt.Ignore())
            .ForMember(dest => dest.DestinationLocation, opt => opt.Ignore())
            .ForMember(dest => dest.EstimatedDistanceKm, opt => opt.Ignore())
            .ForMember(dest => dest.EstimatedEnergyConsumptionKWh, opt => opt.Ignore())
            .ForMember(dest => dest.RequiresPublicCharging, opt => opt.Ignore())
            .ForMember(dest => dest.SuggestedChargingSessionId, opt => opt.Ignore())
            .ForMember(dest => dest.SuggestedChargingSession, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<CreateDriverRequestDto, Driver>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AssignedVehicles, opt => opt.Ignore())
            .ForMember(dest => dest.AssignedTrips, opt => opt.Ignore());
        CreateMap<Driver, CreateDriverResponseDto>();
        CreateMap<CreateLocationRequestDto, Location>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TripsAsOrigin, opt => opt.Ignore())
            .ForMember(dest => dest.TripsAsDestination, opt => opt.Ignore())
            .ForMember(dest => dest.Depots, opt => opt.Ignore());
        CreateMap<Location, CreateLocationResponseDto>();
        CreateMap<CreateVehicleRequestDto, Vehicle>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AssignedDriver, opt => opt.Ignore())
            .ForMember(dest => dest.Trips, opt => opt.Ignore())
            .ForMember(dest => dest.ChargingSessions, opt => opt.Ignore())
            .ForMember(dest => dest.CurrentSoCPercent, opt => opt.Ignore())
            .ForMember(dest => dest.IsAvailable, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.LicensePlate));
        CreateMap<Vehicle, CreateVehicleResponseDto>()
            .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.RegistrationNumber))
            .ForMember(dest => dest.BatteryCapacityKWh, opt => opt.Ignore());
        // Add more mappings as needed
    }
}
