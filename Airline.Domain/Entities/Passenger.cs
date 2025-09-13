using System;

namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a passenger of the airline.
/// </summary>
public class Passenger
{
    public required int Id { get; set; }
    public required string PassportNumber { get; set; }
    public required string FullName { get; set; }
    public required DateTime BirthDate { get; set; }
}
