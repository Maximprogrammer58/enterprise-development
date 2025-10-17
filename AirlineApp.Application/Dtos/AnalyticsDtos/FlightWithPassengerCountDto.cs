namespace AirlineApp.Application.Dtos.AnalyticsDtos;

public class FlightWithPassengerCountDto
{
    public required string FlightCode { get; set; } 
    public required int PassengerCount { get; set; }
}

