namespace AirlineApp.Application.Dtos.AnalyticsDtos;

public class ModelSummaryDto
{
    public string ModelName { get; set; } = null!;
    public int TotalFlights { get; set; }
    public int TotalPassengers { get; set; }
    public double TotalBaggage { get; set; }
}
