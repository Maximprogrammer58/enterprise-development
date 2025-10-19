namespace AirlineApp.Application.Dtos.AnalyticsDtos;

// <summary>
/// DTO representing a flight with its passenger count.
/// </summary>
public class FlightWithPassengerCountDto
{
    /// <summary>Flight code.</summary>
    public required string FlightCode { get; set; }

    /// <summary>Number of passengers on the flight.</summary>
    public required int PassengerCount { get; set; }
}

