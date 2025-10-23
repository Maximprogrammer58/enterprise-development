using AirlineApp.Contracts.Dtos.AircraftFamilyDtos;
using FluentValidation;

namespace AirlineApp.Contracts.Validators;

/// <summary>
/// Validator for AircraftFamilyEditDto.
/// Ensures the correctness of data when creating or updating an aircraft family.
/// </summary>
public class AircraftFamilyEditDtoValidator : AbstractValidator<AircraftFamilyEditDto>
{
    public AircraftFamilyEditDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must not exceed 128 characters.");

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required.")
            .MaximumLength(128).WithMessage("Manufacturer must not exceed 128 characters.");
    }
}
