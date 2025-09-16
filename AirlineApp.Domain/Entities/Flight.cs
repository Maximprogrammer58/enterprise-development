namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a flight with departure, arrival, duration, and associated aircraft model.
/// </summary>
public class Flight
{
    /// <summary>
    /// Gets or sets the unique identifier of the flight.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Gets or sets the flight code (e.g., FL001).
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Gets or sets the departure city or airport.
    /// </summary>
    public required string Departure { get; set; }

    /// <summary>
    /// Gets or sets the arrival city or airport.
    /// </summary>
    public required string Arrival { get; set; }

    /// <summary>
    /// Gets or sets the date and time of departure.
    /// </summary>
    public DateTime? DepartureDateTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time of arrival.
    /// </summary>
    public DateTime? ArrivalDateTime { get; set; }

    /// <summary>
    /// Gets or sets the flight duration.
    /// </summary>
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// Gets or sets the aircraft model used for this flight.
    /// </summary>
    public required AircraftModel AircraftModel { get; set; }
}
