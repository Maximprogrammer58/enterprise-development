namespace AirlineApp.Application.Dtos.FlightDtos;

public class FlightEditDto
{
    public required string Code { get; set; }
    public required string Departure { get; set; }
    public required string Arrival { get; set; }
    public DateTime? DepartureDateTime { get; set; }
    public DateTime? ArrivalDateTime { get; set; }
    public TimeSpan? Duration { get; set; }
    public required int AircraftModelId { get; set; }
}
