namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents an aircraft model with specifications and capacity.
/// </summary>
public class AircraftModel
{
    /// <summary>
    /// Unique identifier of the aircraft model.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the aircraft model.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Aircraft family this model belongs to.
    /// </summary>
    public required AircraftFamily Family { get; set; }

    /// <summary>
    /// Flight range of a specific aircraft model.
    /// </summary>
    public required float FlightRange { get; set; }

    /// <summary>
    /// Passenger capacity of the aircraft model.
    /// </summary>
    public required float PassengerCapacity { get; set; }

    /// <summary>
    /// Cargo capacity of the aircraft model.
    /// </summary>
    public required float CargoCapacity { get; set; }

    /// <summary>
    /// Collection of aircraft models that belong to this family.
    /// </summary>
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
