namespace AirlineApp.Application.Dtos.TicketDtos;

public class TicketEditDto
{
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public bool HasHandLuggage { get; set; }
    public double? BaggageWeight { get; set; }
}