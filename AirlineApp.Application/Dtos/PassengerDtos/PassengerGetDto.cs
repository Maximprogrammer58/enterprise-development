namespace AirlineApp.Application.Dtos.PassengerDtos;

public class PassengerGetDto
{
    public int Id { get; set; }
    public required string PassportNumber { get; set; }
    public required string FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
}
