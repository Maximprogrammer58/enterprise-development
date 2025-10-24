using AirlineApp.Application.Services;
using AirlineApp.Contracts.Dtos.AircraftFamilyDtos;
using AirlineApp.Contracts.Dtos.AircraftModelDtos;
using AirlineApp.Contracts.Dtos.FlightDtos;
using AirlineApp.Contracts.Dtos.PassengerDtos;
using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Contracts.Interfaces;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AirlineApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAircraftFamilyRepository, AircraftFamilyRepository>();
        services.AddScoped<IAircraftModelRepository, AircraftModelRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IPassengerRepository, PassengerRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        services.AddScoped<ICrudService<AircraftFamilyGetDto, AircraftFamilyEditDto>, AircraftFamilyService>();
        services.AddScoped<ICrudService<AircraftModelGetDto, AircraftModelEditDto>, AircraftModelService>();
        services.AddScoped<ICrudService<FlightGetDto, FlightEditDto>, FlightService>();
        services.AddScoped<ICrudService<PassengerGetDto, PassengerEditDto>, PassengerService>();
        services.AddScoped<ICrudService<TicketGetDto, TicketEditDto>, TicketService>();

        services.AddScoped<AnalyticsService>();

        return services;
    }
}
