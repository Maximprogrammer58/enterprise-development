namespace AirlineApp.Contracts.Dtos.PassengerDtos;

/// <summary>
/// DTO for retrieving Passenger information
/// </summary>
public class PassengerGetDto
{
    /// <summary>
    /// Unique identifier of the passenger.
    /// </summary>
    public int Id { get; set; }

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
