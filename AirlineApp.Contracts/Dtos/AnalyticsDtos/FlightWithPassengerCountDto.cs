namespace AirlineApp.Contracts.Dtos.AnalyticsDtos;

// <summary>
/// DTO representing a flight with its passenger count.
/// </summary>
public class FlightWithPassengerCountDto(string flightCode, int passengerCount)
{
    /// <summary>Flight code.</summary>
    public string FlightCode { get; set; } = flightCode;

    /// <summary>Number of passengers on the flight.</summary>
    public int PassengerCount { get; set; } = passengerCount;
}

