namespace AirlineApp.Application.Dtos.PassengerDtos;

/// <summary>
/// DTO for creating or updating Passenger
/// </summary>
public class PassengerEditDto
{
    /// <summary>
    /// Passport number of the passenger.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the passenger.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Birth date of the passenger.
    /// </summary>
    public DateOnly? BirthDate { get; set; }
}