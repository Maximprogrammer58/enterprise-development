namespace AirlineApp.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// DTO representing a passenger with zero baggage on a flight.
/// </summary>
public class PassengerWithZeroBaggageDto
{
    /// <summary>Passenger full name.</summary>
    public required string PassengerName { get; set; }

    /// <summary>Flight code the passenger is on.</summary>
    public required string FlightCode { get; set; }
}
