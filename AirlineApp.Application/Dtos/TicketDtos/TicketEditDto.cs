namespace AirlineApp.Application.Dtos.TicketDtos;

/// <summary>
/// DTO for creating or updating Ticket
/// </summary>
public class TicketEditDto
{
    /// <summary>
    /// Id of the flight associated with this ticket.
    /// </summary>
    public required int FlightId { get; set; }

    /// <summary>
    /// Id of the passenger who holds this ticket.
    /// </summary>
    public required int PassengerId { get; set; }

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