namespace AirlineApp.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// DTO representing a passenger with zero baggage on a flight.
/// </summary>
public class PassengerWithZeroBaggageDto(string passengerName, string flightCode)
{
    /// <summary>Passenger full name.</summary>
    public string PassengerName { get; set; } = passengerName;

    /// <summary>Flight code the passenger is on.</summary>
    public string FlightCode { get; set; } = flightCode;
}
