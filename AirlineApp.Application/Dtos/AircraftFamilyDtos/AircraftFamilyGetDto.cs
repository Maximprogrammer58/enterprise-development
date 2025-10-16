namespace AirlineApp.Application.Dtos.AircraftFamilyDtos;

public class AircraftFamilyGetDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Manufacturer { get; set; }
}