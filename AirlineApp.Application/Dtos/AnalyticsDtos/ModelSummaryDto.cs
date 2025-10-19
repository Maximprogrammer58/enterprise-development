namespace AirlineApp.Application.Dtos.AnalyticsDtos;

/// <summary>
/// DTO representing summary information of a model's flights in a period.
/// </summary>
public class ModelSummaryDto
{
    /// <summary>Name of the aircraft model.</summary>
    public required string ModelName { get; set; }

    /// <summary>Total number of flights for the model in the period.</summary>
    public required int TotalFlights { get; set; }

    /// <summary>Total number of passengers on all flights of this model in the period.</summary>
    public required int TotalPassengers { get; set; }

    /// <summary>Total baggage weight of all passengers on this model in the period.</summary>
    public required double TotalBaggage { get; set; }
}
