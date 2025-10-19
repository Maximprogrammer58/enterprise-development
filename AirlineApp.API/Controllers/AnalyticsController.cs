using AirlineApp.Application.Dtos.AnalyticsDtos;
using AirlineApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.Api.Controllers;

/// <summary>
/// Provides analytics endpoints for flights, passengers, and aircraft models.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(AnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Retrieves the top 5 flights by passenger count.
    /// </summary>
    /// <returns>A list of <see cref="FlightWithPassengerCountDto"/> representing flights and their passenger counts.</returns>
    [HttpGet("top-flights")]
    public async Task<ActionResult<List<FlightWithPassengerCountDto>>> GetTopFlights()
        => Ok(await analyticsService.GetTopFlightsByPassengerCountAsync());

    /// <summary>
    /// Retrieves passengers with zero baggage for a specific flight.
    /// </summary>
    /// <param name="flightCode">The code of the flight.</param>
    /// <returns>A list of <see cref="PassengerWithZeroBaggageDto"/>.</returns>
    [HttpGet("passengers-zero-baggage")]
    public async Task<ActionResult<List<PassengerWithZeroBaggageDto>>> GetPassengersZeroBaggage([FromQuery] string flightCode)
        => Ok(await analyticsService.GetPassengersWithZeroBaggageAsync(flightCode));

    /// <summary>
    /// Retrieves summary information about a model's flights in a specified period.
    /// </summary>
    /// <param name="modelName">The name of the aircraft model.</param>
    /// <param name="start">Start date of the period.</param>
    /// <param name="end">End date of the period.</param>
    /// <returns>A <see cref="ModelSummaryDto"/> containing flight count, passenger count, and total baggage.</returns>
    [HttpGet("model-summary")]
    public async Task<ActionResult<ModelSummaryDto>> GetModelSummary([FromQuery] string modelName, [FromQuery] DateTime start, [FromQuery] DateTime end)
        => Ok(await analyticsService.GetSummaryByModelInPeriodAsync(modelName, start, end));

    /// <summary>
    /// Retrieves flight codes from a specific departure to arrival location.
    /// </summary>
    /// <param name="departure">Departure location.</param>
    /// <param name="arrival">Arrival location.</param>
    /// <returns>A list of flight codes.</returns>
    [HttpGet("flights-from-to")]
    public async Task<ActionResult<List<string>>> GetFlightsFromTo([FromQuery] string departure, [FromQuery] string arrival)
        => Ok(await analyticsService.GetFlightsFromToAsync(departure, arrival));

    /// <summary>
    /// Retrieves flights with the minimal duration.
    /// </summary>
    /// <returns>A list of flight codes with the shortest duration.</returns>
    [HttpGet("flights-min-duration")]
    public async Task<ActionResult<List<string>>> GetFlightsWithMinimalDuration()
        => Ok(await analyticsService.GetFlightsWithMinimalDurationAsync());
}
