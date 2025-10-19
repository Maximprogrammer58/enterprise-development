namespace AirlineApp.Application.Dtos.AircraftFamilyDtos;

/// <summary>
/// DTO for retrieving AircraftFamily information
/// </summary>
public class AircraftFamilyGetDto
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
}