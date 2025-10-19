namespace AirlineApp.Application.Dtos.FlightDtos;

/// <summary>
/// DTO for creating or updating Flight
/// </summary>
public class FlightEditDto
{
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
    /// Id of the aircraft model used for this flight.
    /// </summary>
    public required int AircraftModelId { get; set; }
}
