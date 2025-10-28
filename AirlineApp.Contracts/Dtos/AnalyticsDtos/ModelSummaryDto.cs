namespace AirlineApp.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// DTO representing summary information of a model's flights in a period.
/// </summary>
public class ModelSummaryDto(string modelName, int totalFlights, int totalPassengers, double totalBaggage)
{
    /// <summary>Name of the aircraft model.</summary>
    public string ModelName { get; set; } = modelName;

    /// <summary>Total number of flights for the model in the period.</summary>
    public int TotalFlights { get; set; } = totalFlights;

    /// <summary>Total number of passengers on all flights of this model in the period.</summary>
    public int TotalPassengers { get; set; } = totalPassengers;

    /// <summary>Total baggage weight of all passengers on this model in the period.</summary>
    public double TotalBaggage { get; set; } = totalBaggage;
}
