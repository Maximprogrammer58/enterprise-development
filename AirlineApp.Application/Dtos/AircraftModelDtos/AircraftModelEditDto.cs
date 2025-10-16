namespace AirlineApp.Application.Dtos.AircraftModelDtos;

public class AircraftModelEditDto
{
    public required string Name { get; set; }
    public required float FlightRange { get; set; }
    public required float PassengerCapacity { get; set; }
    public required float CargoCapacity { get; set; }
    public required int FamilyId { get; set; }
}