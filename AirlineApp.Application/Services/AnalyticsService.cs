using AirlineApp.Application.Dtos.AnalyticsDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;

namespace AirlineApp.Application.Services;

public class AnalyticsService
{
    private readonly IFlightRepository _flightRepository;
    private readonly ITicketRepository _ticketRepository;

    public AnalyticsService(
        IFlightRepository flightRepository,
        ITicketRepository ticketRepository)
    {
        _flightRepository = flightRepository;
        _ticketRepository = ticketRepository;
    }

    // 1️⃣ Топ 5 рейсов по количеству пассажиров
    public async Task<List<FlightWithPassengerCountDto>> GetTopFlightsByPassengerCountAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        return tickets
            .GroupBy(t => t.Flight)
            .Select(g => new FlightWithPassengerCountDto
            {
                FlightCode = g.Key.Code,
                PassengerCount = g.Count()
            })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .ToList();
    }

    // 2️⃣ Пассажиры с нулевым багажом на рейсе
    public async Task<List<PassengerWithZeroBaggageDto>> GetPassengersWithZeroBaggageAsync(string flightCode)
    {
        var tickets = await _ticketRepository.GetAllAsync();

        return tickets
            .Where(t => t.Flight.Code == flightCode && (t.BaggageWeight ?? 0) == 0)
            .Select(t => new PassengerWithZeroBaggageDto
            {
                PassengerName = t.Passenger.FullName,
                FlightCode = t.Flight.Code
            })
            .OrderBy(p => p.PassengerName)
            .ToList();
    }

    // 3️⃣ Суммарная информация по модели самолетов за период
    public async Task<ModelSummaryDto> GetSummaryByModelInPeriodAsync(string modelName, DateTime start, DateTime end)
    {
        var flights = (await _flightRepository.GetAllAsync())
            .Where(f => f.AircraftModel.Name == modelName &&
                        f.DepartureDateTime.HasValue && f.ArrivalDateTime.HasValue &&
                        f.DepartureDateTime.Value >= start && f.ArrivalDateTime.Value <= end)
            .ToList();

        var tickets = (await _ticketRepository.GetAllAsync())
            .Where(t => flights.Contains(t.Flight))
            .ToList();

        return new ModelSummaryDto
        {
            ModelName = modelName,
            TotalFlights = flights.Count,
            TotalPassengers = tickets.Count,
            TotalBaggage = tickets.Sum(t => t.BaggageWeight ?? 0)
        };
    }

    // 4️⃣ Рейсы из города A в город B
    public async Task<List<string>> GetFlightsFromToAsync(string departure, string arrival)
    {
        var flights = await _flightRepository.GetAllAsync();
        return flights
            .Where(f => f.Departure == departure && f.Arrival == arrival)
            .OrderBy(f => f.Code)
            .Select(f => f.Code)
            .ToList();
    }

    // 5️⃣ Рейсы с минимальной продолжительностью
    public async Task<List<string>> GetFlightsWithMinimalDurationAsync()
    {
        var flights = await _flightRepository.GetAllAsync();
        var minDuration = flights
            .Where(f => f.Duration.HasValue)
            .Min(f => f.Duration!.Value);

        return flights
            .Where(f => f.Duration.HasValue && f.Duration.Value == minDuration)
            .OrderBy(f => f.Code)
            .Select(f => f.Code)
            .ToList();
    }
}