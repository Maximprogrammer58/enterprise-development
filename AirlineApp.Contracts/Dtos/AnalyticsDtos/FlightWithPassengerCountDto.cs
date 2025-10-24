namespace AirlineApp.Contracts.Dtos.AnalyticsDtos;

// <summary>
/// DTO representing a flight with its passenger count.
/// </summary>
public class FlightWithPassengerCountDto
{
    /// <summary>Flight code.</summary>
    public string FlightCode { get; set; }

    /// <summary>Number of passengers on the flight.</summary>
    public int PassengerCount { get; set; }

    public FlightWithPassengerCountDto(string flightCode, int passengerCount)
    {
        FlightCode = flightCode;
        PassengerCount = passengerCount;
    }
}

