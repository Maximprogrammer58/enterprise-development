namespace AirlineApp.Contracts.Dtos.AircraftFamilyDtos;

/// <summary>
/// DTO for creating or updating AircraftFamily
/// </summary>
public class AircraftFamilyEditDto
{
    /// <summary>
    /// Name of the aircraft family.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Manufacturer of the aircraft family.
    /// </summary>
    public required string Manufacturer { get; set; }
}