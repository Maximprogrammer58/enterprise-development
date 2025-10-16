namespace AirlineApp.Application.Dtos.AircraftModelDtos;

public class AircraftModelEditDto
{
    /// <summary>
    /// Name of the aircraft model.
    /// </summary>
    public required string Name { get; set; }
    public required float FlightRange { get; set; }

    /// <summary>
    /// Passenger capacity of the aircraft model.
    /// </summary>
    public required float PassengerCapacity { get; set; }

    /// <summary>
    /// Cargo capacity of the aircraft model.
    /// </summary>
    public required float CargoCapacity { get; set; }
    public required string FamilyName { get; set; }
}