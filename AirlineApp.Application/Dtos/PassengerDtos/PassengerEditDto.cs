namespace AirlineApp.Application.Dtos.PassengerDtos;

public class PassengerEditDto
{
    public string FullName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
