using System;

namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents an aircraft model with specifications and capacity.
/// Linked to a specific aircraft family.
/// </summary>
public class AircraftModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public required int AircraftFamilyId { get; set; }
    public AircraftFamily? Family { get; set; }

    public required int FlightRange { get; set; }
    public required int PassengerCapacity { get; set; }
    public required int CargoCapacity { get; set; }
}
