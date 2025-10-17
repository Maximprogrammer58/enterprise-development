namespace AirlineApp.Application.Dtos.AnalyticsDtos;

public class ModelSummaryDto
{
    public required string ModelName { get; set; } 
    public required int TotalFlights { get; set; }
    public required int TotalPassengers { get; set; }
    public required double TotalBaggage { get; set; }
}
