namespace AirlineApp.Application.Dtos.AircraftModelDtos;

public class AircraftModelGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int FlightRange { get; set; }
    public int PassengerCapacity { get; set; }
    public int CargoCapacity { get; set; }
    public string FamilyName { get; set; } = string.Empty;
}
