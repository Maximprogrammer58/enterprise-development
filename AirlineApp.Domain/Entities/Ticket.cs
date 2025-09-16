namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a ticket, linking a passenger to a flight.
/// </summary>
public class Ticket
{
    /// <summary>
    /// Gets or sets the unique identifier of the ticket.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Gets or sets the flight associated with this ticket.
    /// </summary>
    public required Flight Flight { get; set; }

    /// <summary>
    /// Gets or sets the passenger who holds this ticket.
    /// </summary>
    public required Passenger Passenger { get; set; }

    /// <summary>
    /// Gets or sets the seat number assigned to the passenger.
    /// </summary>
    public required string SeatNumber { get; set; }

    /// <summary>
    /// Indicates whether the passenger has hand luggage.
    /// </summary>
    public required bool HasHandLuggage { get; set; }

    /// <summary>
    /// Gets or sets the checked baggage weight in kilograms.
    /// Nullable if the passenger has no baggage.
    /// </summary>
    public double? BaggageWeight { get; set; }
}