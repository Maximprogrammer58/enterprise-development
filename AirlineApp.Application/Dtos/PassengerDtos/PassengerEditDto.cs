namespace AirlineApp.Application.Dtos.PassengerDtos;

public class PassengerEditDto
{
    public required string PassportNumber { get; set; }
    public required string FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
}