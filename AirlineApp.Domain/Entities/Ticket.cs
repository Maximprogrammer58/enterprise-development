namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a ticket, linking a passenger to a flight.
/// </summary>
public class Ticket
{
    /// <summary>
    /// Unique identifier of the ticket.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Flight associated with this ticket.
    /// </summary>
    public required Flight Flight { get; set; }

    /// <summary>
    /// Passenger who holds this ticket.
    /// </summary>
    public required Passenger Passenger { get; set; }

    /// <summary>
    /// Seat number assigned to the passenger.
    /// </summary>
    public required string SeatNumber { get; set; }

    /// <summary>
    /// Flag indicating whether the passenger has hand luggage.
    /// </summary>
    public required bool HasHandLuggage { get; set; }

    /// <summary>
    /// Baggage weight.
    /// </summary>
    public double? BaggageWeight { get; set; }
}