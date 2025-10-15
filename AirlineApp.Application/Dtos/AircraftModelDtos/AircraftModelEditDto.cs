namespace AirlineApp.Application.Dtos.AircraftModelDtos;

public class AircraftModelEditDto
{
    public string Name { get; set; } = string.Empty;
    public int FlightRange { get; set; }
    public int PassengerCapacity { get; set; }
    public int CargoCapacity { get; set; }
    public int FamilyId { get; set; }
}