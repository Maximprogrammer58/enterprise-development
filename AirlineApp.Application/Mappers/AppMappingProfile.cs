using AutoMapper;
using AirlineApp.Domain.Entities;
using AirlineApp.Application.Dtos.AircraftFamilyDtos;
using AirlineApp.Application.Dtos.AircraftModelDtos;
using AirlineApp.Application.Dtos.FlightDtos;
using AirlineApp.Application.Dtos.PassengerDtos;
using AirlineApp.Application.Dtos.TicketDtos;

namespace AirlineApp.Application.Mappers;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // AircraftFamily
        CreateMap<AircraftFamily, AircraftFamilyGetDto>().ReverseMap();
        CreateMap<AircraftFamilyEditDto, AircraftFamily>().ReverseMap();

        // AircraftModel
        CreateMap<AircraftModel, AircraftModelGetDto>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ReverseMap();
        CreateMap<AircraftModelEditDto, AircraftModel>().ReverseMap();

        // Flight
        CreateMap<Flight, FlightGetDto>()
            .ForMember(dest => dest.AircraftModelName, opt => opt.MapFrom(src => src.AircraftModel.Name))
            .ReverseMap();
        CreateMap<FlightEditDto, Flight>().ReverseMap();

        // Passenger
        CreateMap<Passenger, PassengerGetDto>().ReverseMap();
        CreateMap<PassengerEditDto, Passenger>().ReverseMap();

        // Ticket
        CreateMap<Ticket, TicketGetDto>()
            .ForMember(dest => dest.FlightCode, opt => opt.MapFrom(src => src.Flight.Code))
            .ForMember(dest => dest.PassengerName, opt => opt.MapFrom(src => src.Passenger.FullName))
            .ReverseMap();

        CreateMap<TicketEditDto, Ticket>().ReverseMap();
    }
}
