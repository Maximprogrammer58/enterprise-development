namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a flight with departure, arrival, duration, and associated aircraft model.
/// </summary>
public class Flight
{
    /// <summary>
    /// Unique identifier of the flight.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Flight code
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Point of departure.
    /// </summary>
    public required string Departure { get; set; }

    /// <summary>
    /// Point of arrival.
    /// </summary>
    public required string Arrival { get; set; }

    /// <summary>
    /// Date and time of departure.
    /// </summary>
    public DateTime? DepartureDateTime { get; set; }

    /// <summary>
    /// Date and time of arrival.
    /// </summary>
    public DateTime? ArrivalDateTime { get; set; }

    /// <summary>
    /// Flight duration.
    /// </summary>
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// Aircraft model used for this flight.
    /// </summary>
    public required AircraftModel AircraftModel { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
