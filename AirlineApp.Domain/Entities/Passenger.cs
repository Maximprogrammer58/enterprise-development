namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a passenger of the airline.
/// </summary>
public class Passenger
{
    /// <summary>
    /// Gets or sets the unique identifier of the passenger.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Gets or sets the passport number of the passenger.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Gets or sets the full name of the passenger.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the birth date of the passenger.
    /// Nullable to allow creating a passenger without a known birth date.
    /// </summary>
    public DateOnly? BirthDate { get; set; }
}
