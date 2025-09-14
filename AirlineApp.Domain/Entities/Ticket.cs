using System;

namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a ticket, linking a passenger to a flight.
/// </summary>
public class Ticket
{
    public required int Id { get; set; }

    public required int FlightId { get; set; }
    public Flight? Flight { get; set; }

    public required int PassengerId { get; set; }
    public Passenger? Passenger { get; set; }

    public required string SeatNumber { get; set; }
    public required bool HasHandLuggage { get; set; }
    public double? BaggageWeight { get; set; }
}
