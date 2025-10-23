using AutoMapper;
using AirlineApp.Domain.Entities;
using AirlineApp.Contracts.Dtos.AircraftFamilyDtos;
using AirlineApp.Contracts.Dtos.AircraftModelDtos;
using AirlineApp.Contracts.Dtos.FlightDtos;
using AirlineApp.Contracts.Dtos.PassengerDtos;
using AirlineApp.Contracts.Dtos.TicketDtos;

namespace AirlineApp.Application.Mappers;

/// <summary>
/// AutoMapper profile for mapping between domain entities and DTOs.
/// Includes mappings for AircraftFamily, AircraftModel, Flight, Passenger, and Ticket.
/// </summary>
public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<AircraftFamily, AircraftFamilyGetDto>().ReverseMap();
        CreateMap<AircraftFamilyEditDto, AircraftFamily>().ReverseMap();

        CreateMap<AircraftModel, AircraftModelGetDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ReverseMap();
        CreateMap<AircraftModelEditDto, AircraftModel>().ReverseMap();

        CreateMap<Flight, FlightGetDto>()
            .ForMember(dest => dest.AircraftModelName, opt => opt.MapFrom(src => src.AircraftModel.Name))
            .ReverseMap();
        CreateMap<FlightEditDto, Flight>().ReverseMap();

        CreateMap<Passenger, PassengerGetDto>().ReverseMap();
        CreateMap<PassengerEditDto, Passenger>().ReverseMap();

        CreateMap<Ticket, TicketGetDto>()
            .ForMember(dest => dest.FlightCode, opt => opt.MapFrom(src => src.Flight.Code))
            .ForMember(dest => dest.PassengerName, opt => opt.MapFrom(src => src.Passenger.FullName))
            .ReverseMap();

        CreateMap<TicketEditDto, Ticket>().ReverseMap();
    }
}
