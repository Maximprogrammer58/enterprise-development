using AirlineApp.Contracts.Dtos.AircraftFamilyDtos;
using AirlineApp.Contracts.Dtos.AircraftModelDtos;
using AirlineApp.Contracts.Dtos.FlightDtos;
using AirlineApp.Contracts.Dtos.PassengerDtos;
using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Contracts.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirlineApp.Application.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddTransient<IValidator<AircraftFamilyEditDto>, AircraftFamilyEditDtoValidator>();
        services.AddTransient<IValidator<AircraftModelEditDto>, AircraftModelEditDtoValidator>();
        services.AddTransient<IValidator<FlightEditDto>, FlightEditDtoValidator>();
        services.AddTransient<IValidator<PassengerEditDto>, PassengerEditDtoValidator>();
        services.AddTransient<IValidator<TicketEditDto>, TicketEditDtoValidator>();

        return services;
    }
}
