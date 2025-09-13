using System;

namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a flight with departure, arrival, duration, and associated aircraft model.
/// Duration is stored as a property to allow database mapping.
/// </summary>
public class Flight
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Departure { get; set; }
    public required string Arrival { get; set; }

    public required DateTime DepartureDateTime { get; set; }
    public required DateTime ArrivalDateTime { get; set; }
    public required TimeSpan Duration { get; set; }

    public required int AircraftModelId { get; set; }
    public AircraftModel? AircraftModel { get; set; }
}
