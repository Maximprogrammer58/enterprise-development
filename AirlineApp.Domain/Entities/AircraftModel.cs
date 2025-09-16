namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents an aircraft model with specifications and capacity.
/// Linked to a specific aircraft family.
/// </summary>
public class AircraftModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the aircraft model.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the aircraft model (e.g., A320, 737-800).
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the aircraft family this model belongs to.
    /// </summary>
    public required AircraftFamily Family { get; set; }

    /// <summary>
    /// Gets or sets the maximum flight range in kilometers.
    /// </summary>
    public required float FlightRange { get; set; }

    /// <summary>
    /// Gets or sets the passenger capacity of the aircraft model.
    /// </summary>
    public required float PassengerCapacity { get; set; }

    /// <summary>
    /// Gets or sets the cargo capacity of the aircraft model in kilograms.
    /// </summary>
    public required float CargoCapacity { get; set; }
}
