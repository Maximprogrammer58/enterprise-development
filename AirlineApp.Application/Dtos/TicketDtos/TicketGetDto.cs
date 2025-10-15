namespace AirlineApp.Application.Dtos.TicketDtos;

public class TicketGetDto
{
    public int Id { get; set; }
    public string FlightCode { get; set; } = string.Empty;
    public string PassengerName { get; set; } = string.Empty;
    public string SeatNumber { get; set; } = string.Empty;
    public bool HasHandLuggage { get; set; }
    public double? BaggageWeight { get; set; }
}
