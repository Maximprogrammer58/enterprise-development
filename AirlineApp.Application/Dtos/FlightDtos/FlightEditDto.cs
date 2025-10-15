namespace AirlineApp.Application.Dtos.FlightDtos;

public class FlightEditDto
{
    public string Code { get; set; } = string.Empty;
    public string Departure { get; set; } = string.Empty;
    public string Arrival { get; set; } = string.Empty;
    public DateTime DepartureDateTime { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int AircraftModelId { get; set; }
}
