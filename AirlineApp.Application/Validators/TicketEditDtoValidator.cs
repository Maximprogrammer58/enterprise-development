using AirlineApp.Application.Dtos.TicketDtos;
using FluentValidation;

namespace AirlineApp.Application.Validators;

/// <summary>
/// Validator for PassengerEditDto.
/// Ensures that data is correct when creating or updating a passenger.
/// </summary>
public class TicketEditDtoValidator : AbstractValidator<TicketEditDto>
{
    public TicketEditDtoValidator()
    {
        RuleFor(x => x.FlightId)
            .GreaterThan(0).WithMessage("FlightId must be greater than 0.");

        RuleFor(x => x.PassengerId)
            .GreaterThan(0).WithMessage("PassengerId must be greater than 0.");

        RuleFor(x => x.SeatNumber)
            .NotEmpty().WithMessage("Seat number is required.")
            .MaximumLength(5).WithMessage("Seat number must not exceed 5 characters.");

        RuleFor(x => x.BaggageWeight)
            .GreaterThanOrEqualTo(0).When(x => x.BaggageWeight.HasValue)
            .WithMessage("Baggage weight cannot be negative.");
    }
}
