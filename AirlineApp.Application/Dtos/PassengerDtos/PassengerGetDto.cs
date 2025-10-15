namespace AirlineApp.Application.Dtos.PassengerDtos;

public class PassengerGetDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
