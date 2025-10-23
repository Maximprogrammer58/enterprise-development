namespace AirlineApp.Contracts.Dtos.TicketDtos;

/// <summary>
/// DTO for retrieving Ticket information
/// </summary>
public class TicketGetDto
{
    /// <summary>
    /// Unique identifier of the ticket.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the flight associated with this ticket.
    /// </summary>
    public required int FlightId { get; set; }
    /// <summary>
    /// Code of the flight associated with this ticket.
    /// </summary>
    public required string FlightCode { get; set; }

    /// <summary>
    /// Id of the passenger who holds this ticket.
    /// </summary>
    public required int PassengerId { get; set; }

    /// <summary>
    /// Name of the passenger who holds this ticket.
    /// </summary>
    public required string PassengerName { get; set; }

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