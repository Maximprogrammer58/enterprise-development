namespace AirlineApp.Application.Dtos.FlightDtos;

public class FlightGetDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Departure { get; set; } = string.Empty;
    public string Arrival { get; set; } = string.Empty;
    public DateTime DepartureDateTime { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string AircraftModelName { get; set; } = string.Empty;
}