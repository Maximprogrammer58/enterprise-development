namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a passenger of the airline.
/// </summary>
public class Passenger
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

    /// <summary>
    /// Collection of tickets associated with this passneger.
    /// </summary>
    public ICollection<Ticket> Tickets { get; set; } = [];
}
