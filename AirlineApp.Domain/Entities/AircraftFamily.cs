using System;

namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a family of aircraft, including manufacturer information.
/// </summary>
public class AircraftFamily
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Manufacturer { get; set; }
}
