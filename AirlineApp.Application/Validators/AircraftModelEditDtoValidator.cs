using AirlineApp.Application.Dtos.AircraftModelDtos;
using FluentValidation;

namespace AirlineApp.Application.Validators;

/// <summary>
/// Validator for AircraftModelEditDto.
/// Ensures the correctness of data when creating or updating an aircraft model.
/// </summary>
public class AircraftModelEditDtoValidator : AbstractValidator<AircraftModelEditDto>
{
    public AircraftModelEditDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(64);

        RuleFor(x => x.FlightRange)
            .GreaterThan(0).WithMessage("FlightRange must be greater than 0.");

        RuleFor(x => x.PassengerCapacity)
            .GreaterThan(0).WithMessage("PassengerCapacity must be greater than 0.");

        RuleFor(x => x.CargoCapacity)
            .GreaterThanOrEqualTo(0).WithMessage("CargoCapacity cannot be negative.");

        RuleFor(x => x.FamilyId)
            .GreaterThan(0).WithMessage("FamilyId must be a valid positive number.");
    }
}
