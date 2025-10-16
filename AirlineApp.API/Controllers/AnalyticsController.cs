using AirlineApp.Application.Dtos.AnalyticsDtos;
using AirlineApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("top-flights")]
    public async Task<ActionResult<List<FlightWithPassengerCountDto>>> GetTopFlights()
        => Ok(await _analyticsService.GetTopFlightsByPassengerCountAsync());

    [HttpGet("passengers-zero-baggage")]
    public async Task<ActionResult<List<PassengerWithZeroBaggageDto>>> GetPassengersZeroBaggage([FromQuery] string flightCode)
        => Ok(await _analyticsService.GetPassengersWithZeroBaggageAsync(flightCode));

    [HttpGet("model-summary")]
    public async Task<ActionResult<ModelSummaryDto>> GetModelSummary([FromQuery] string modelName, [FromQuery] DateTime start, [FromQuery] DateTime end)
        => Ok(await _analyticsService.GetSummaryByModelInPeriodAsync(modelName, start, end));

    [HttpGet("flights-from-to")]
    public async Task<ActionResult<List<string>>> GetFlightsFromTo([FromQuery] string departure, [FromQuery] string arrival)
        => Ok(await _analyticsService.GetFlightsFromToAsync(departure, arrival));

    [HttpGet("flights-min-duration")]
    public async Task<ActionResult<List<string>>> GetFlightsWithMinimalDuration()
        => Ok(await _analyticsService.GetFlightsWithMinimalDurationAsync());
}
