using AirlineApp.Contracts.Dtos.FlightDtos;
using FluentValidation;

namespace AirlineApp.Contracts.Validators;

/// <summary>
/// Validator for FlightEditDto.
/// Ensures the correctness of data when creating or updating an aircraft flight.
/// </summary>
public class FlightEditDtoValidator : AbstractValidator<FlightEditDto>
{
    public FlightEditDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Flight code is required.")
            .MaximumLength(16).WithMessage("Flight code must not exceed 16 characters.");

        RuleFor(x => x.Departure)
            .NotEmpty().WithMessage("Departure is required.")
            .MaximumLength(64);

        RuleFor(x => x.Arrival)
            .NotEmpty().WithMessage("Arrival is required.")
            .MaximumLength(64);

        RuleFor(x => x.DepartureDateTime)
            .NotNull().WithMessage("DepartureDateTime is required.")
            .LessThan(x => x.ArrivalDateTime)
            .WithMessage("Departure time must be before arrival time.");

        RuleFor(x => x.ArrivalDateTime)
            .NotNull().WithMessage("ArrivalDateTime is required.");

        RuleFor(x => x.AircraftModelId)
            .GreaterThan(0).WithMessage("AircraftModelId must be greater than 0.");
    }
}
