namespace AirlineApp.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// DTO representing a passenger with zero baggage on a flight.
/// </summary>
public class PassengerWithZeroBaggageDto
{
    /// <summary>Passenger full name.</summary>
    public string PassengerName { get; set; }

    /// <summary>Flight code the passenger is on.</summary>
    public string FlightCode { get; set; }

    public PassengerWithZeroBaggageDto(string passengerName, string flightCode)
    {
        PassengerName = passengerName;
        FlightCode = flightCode;
    }
}
