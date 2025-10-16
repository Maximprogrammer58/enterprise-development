namespace AirlineApp.Application.Dtos.TicketDtos;

public class TicketGetDto
{
    public int Id { get; set; }
    public required int FlightId { get; set; }
    public required string FlightCode { get; set; }
    public required int PassengerId { get; set; }
    public required string PassengerName { get; set; }
    public required string SeatNumber { get; set; }
    public required bool HasHandLuggage { get; set; }
    public double? BaggageWeight { get; set; }
}