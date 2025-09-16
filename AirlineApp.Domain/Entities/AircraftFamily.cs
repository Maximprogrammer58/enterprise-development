namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a family of aircraft, including manufacturer information.
/// </summary>
public class AircraftFamily
{
    /// <summary>
    /// Gets or sets the unique identifier of the aircraft family.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the aircraft family (e.g., A320 Family).
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer of the aircraft family (e.g., Airbus, Boeing).
    /// </summary>
    public required string Manufacturer { get; set; }
}
