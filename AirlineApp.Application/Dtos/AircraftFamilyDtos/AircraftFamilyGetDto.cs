namespace AirlineApp.Application.Dtos.AircraftFamilyDtos;

public class AircraftFamilyGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
}