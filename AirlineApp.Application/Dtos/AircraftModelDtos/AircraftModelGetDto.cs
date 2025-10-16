namespace AirlineApp.Application.Dtos.AircraftModelDtos;

public class AircraftModelGetDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required float FlightRange { get; set; }
    public required float PassengerCapacity { get; set; }
    public required float CargoCapacity { get; set; }
    public required int FamilyId { get; set; }
    public required string FamilyName { get; set; }
}
