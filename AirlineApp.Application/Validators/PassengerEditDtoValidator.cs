using AirlineApp.Application.Dtos.PassengerDtos;
using FluentValidation;

namespace AirlineApp.Application.Validators;

/// <summary>
/// Validator for PassengerEditDto.
/// Ensures that data is correct when creating or updating a passenger.
/// </summary>
public class PassengerEditDtoValidator : AbstractValidator<PassengerEditDto>
{
    public PassengerEditDtoValidator()
    {
        RuleFor(x => x.PassportNumber)
            .NotEmpty().WithMessage("Passport number is required.")
            .MaximumLength(32);

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(128);

        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("Birth date is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Birth date must be in the past.");
    }
}
