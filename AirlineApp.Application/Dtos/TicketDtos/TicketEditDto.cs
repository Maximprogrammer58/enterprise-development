namespace AirlineApp.Application.Dtos.TicketDtos;

public class TicketEditDto
{
    public required int FlightId { get; set; }
    public required int PassengerId { get; set; }
    public required string SeatNumber { get; set; }
    public required bool HasHandLuggage { get; set; }
    public double? BaggageWeight { get; set; }
}