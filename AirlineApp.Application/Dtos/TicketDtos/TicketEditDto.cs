namespace AirlineApp.Application.Dtos.TicketDtos;

public class TicketEditDto
{
    public string FlightCode { get; set; } = string.Empty;
    public string PassengerName { get; set; } = string.Empty;
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