namespace AirlineApp.Domain.Entities;

/// <summary>
/// Represents a family of aircraft, including manufacturer information.
/// </summary>
public class AircraftFamily
{
    /// <summary>
    /// Unique identifier of the aircraft family.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the aircraft family.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Manufacturer of the aircraft family.
    /// </summary>
    public required string Manufacturer { get; set; }

    public ICollection<AircraftModel> Models { get; set; } = new List<AircraftModel>();
}
