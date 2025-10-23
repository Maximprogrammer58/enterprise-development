namespace AirlineApp.Contracts.Dtos.AircraftModelDtos;

/// <summary>
/// DTO for creating or updating AircraftModel
/// </summary>
public class AircraftModelEditDto
{
    /// <summary>
    /// Name of the aircraft model.
    /// </summary>
    public required string Name { get; set; }

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
    /// Id of the aircraft family this model belongs to.
    /// </summary>
    public required int FamilyId { get; set; }
}