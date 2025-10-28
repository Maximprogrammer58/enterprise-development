using AirlineApp.Contracts.Dtos.AnalyticsDtos;
using AirlineApp.Domain.Interfaces;

namespace AirlineApp.Application.Services;

/// <summary>
/// Provides analytics operations for flights, passengers, and aircraft models.
/// </summary>
public class AnalyticsService(IFlightRepository flightRepository,
        ITicketRepository ticketRepository)
{
    /// <summary>
    /// Returns the top 5 flights with the highest number of passengers.
    /// </summary>
    public async Task<List<FlightWithPassengerCountDto>> GetTopFlightsByPassengerCountAsync()
    {
        var tickets = await ticketRepository.GetAllAsync();

        return [.. tickets
            .GroupBy(t => t.Flight)
            .Select(g => new FlightWithPassengerCountDto(g.Key.Code, g.Count()))
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)];
    }

    /// <summary>
    /// Returns passengers with zero baggage for a given flight.
    /// </summary>
    public async Task<List<PassengerWithZeroBaggageDto>> GetPassengersWithZeroBaggageAsync(string flightCode)
    {
        if (string.IsNullOrWhiteSpace(flightCode))
            throw new ArgumentException("Flight code is required");

        var tickets = await ticketRepository.GetAllAsync();

        return [.. tickets
            .Where(t => t.Flight.Code == flightCode && (t.BaggageWeight ?? 0) == 0)
            .Select(t => new PassengerWithZeroBaggageDto(t.Passenger.FullName, t.Flight.Code))
            .OrderBy(p => p.PassengerName)];
    }

    /// <summary>
    /// Returns summary information about flights for a specific model within a date range.
    /// </summary>
    public async Task<ModelSummaryDto> GetSummaryByModelInPeriodAsync(string modelName, DateTime start, DateTime end)
    {
        if (string.IsNullOrWhiteSpace(modelName))
            throw new ArgumentException("Model name is required");

        if (start > end)
            throw new ArgumentException("Start date cannot be after end date");

        var flights = (await flightRepository.GetAllAsync())
            .Where(f => f.AircraftModel.Name == modelName &&
                        f.DepartureDateTime.HasValue && f.ArrivalDateTime.HasValue &&
                        f.DepartureDateTime.Value >= start && f.ArrivalDateTime.Value <= end)
            .ToList();

        var tickets = (await ticketRepository.GetAllAsync())
            .Where(t => flights.Contains(t.Flight))
            .ToList();

        return new ModelSummaryDto(
            modelName,
            flights.Count,
            tickets.Count,
            tickets.Sum(t => t.BaggageWeight ?? 0)
        );
    }

    /// <summary>
    /// Returns flight codes for flights from a specific departure to arrival location.
    /// </summary>
    public async Task<List<string>> GetFlightsFromToAsync(string departure, string arrival)
    {
        if (string.IsNullOrWhiteSpace(departure))
            throw new ArgumentException("Departure location is required");

        if (string.IsNullOrWhiteSpace(arrival))
            throw new ArgumentException("Arrival location is required");

        if (departure.Equals(arrival, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Departure and arrival locations cannot be the same");

        var flights = await flightRepository.GetAllAsync();
        return [.. flights
            .Where(f => f.Departure == departure && f.Arrival == arrival)
            .OrderBy(f => f.Code)
            .Select(f => f.Code)];
    }

    /// <summary>
    /// Returns flight codes of flights with the minimal duration.
    /// </summary>
    public async Task<List<string>> GetFlightsWithMinimalDurationAsync()
    {
        var flights = await flightRepository.GetAllAsync();

        var minDuration = flights
            .Where(f => f.Duration.HasValue)
            .Min(f => f.Duration!.Value);

        return [.. flights
            .Where(f => f.Duration.HasValue && f.Duration.Value == minDuration)
            .OrderBy(f => f.Code)
            .Select(f => f.Code)];
    }
}