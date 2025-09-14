using Xunit;
using System.Linq;
using AirlineApp.Seed;
using AirlineApp.Domain.Entities;
using System.Collections.Generic;
using System.Data;

namespace AirlineApp.Tests;

/// <summary>
/// Tests queries on flights, passengers, and tickets, ensuring correct results.
/// </summary>
public class QueriesTests
{
    private readonly DataSeed _seed;
    private readonly List<AircraftFamily> _families;
    private readonly List<AircraftModel> _models;
    private readonly List<Flight> _flights;
    private readonly List<Passenger> _passengers;
    private readonly List<Ticket> _tickets;

    public QueriesTests()
    {
        _seed = new DataSeed(new DateTime(2025, 9, 1));
        _families = _seed.GetAircraftFamilies();
        _models = _seed.GetAircraftModels(_families);
        _flights = _seed.GetFlights(_models);
        _passengers = _seed.GetPassengers();
        _tickets = _seed.GetTickets(_flights, _passengers);
    }

    /// <summary>
    /// Assignment: Display top 5 flights by the number of passengers transported.
    /// </summary>
    [Fact]
    public void TopFlightsByPassengerCount()
    {
        var expected = new[] { ("FL001", 6), ("FL003", 5), ("FL002", 4), ("FL004", 3), ("FL005", 2) };

        var query = _tickets
            .GroupBy(t => t.Flight)
            .Select(g => new { Flight = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        Assert.Equal(expected.Length, query.Count);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.NotNull(query[i].Flight);
            Assert.Equal(expected[i].Item1, query[i].Flight.Code);
            Assert.Equal(expected[i].Item2, query[i].Count);
        }
    }

    /// <summary>
    /// Assignment: Display a list of flights with the minimal flight duration.
    /// </summary>
    [Fact]
    public void FlightsWithMinimalDuration()
    {
        var expectedCodes = new[] { "FL005", "FL009" };
        var minDuration = _flights.Min(f => f.Duration);

        var query = _flights
            .Where(f => f.Duration == minDuration)
            .OrderBy(f => f.Code)
            .ToList();

        Assert.Equal(expectedCodes.Length, query.Count);
        Assert.Equal(expectedCodes, query.Select(f => f.Code).ToArray());
    }

    /// <summary>
    /// Assignment: Display all passengers on a selected flight whose baggage weight is zero, ordered by full name.
    /// </summary>
    [Fact]
    public void PassengersWithZeroBaggage()
    {
        var expected = new[] { "Boris B", "Kirill K" };
        var flight = _flights.SingleOrDefault(f => f.Code == "FL001");

        Assert.NotNull(flight);

        var query = _tickets
            .Where(t => t.FlightId == flight.Id && (t.BaggageWeight ?? 0) == 0)
            .Select(t => t.Passenger.FullName)
            .OrderBy(name => name)
            .ToList();

        Assert.Equal(expected, query);
    }

    /// <summary>
    /// Assignment: Display summary information for all flights of a selected aircraft model within a specified period.
    /// </summary>
    [Fact]
    public void SummaryInfoModelInPeriod()
    {
        var model = _models.SingleOrDefault(m => m.Name == "A320");

        Assert.NotNull(model);

        var start = _seed.BaseDate;
        var end = _seed.BaseDate.AddDays(1);

        var flights = _flights
            .Where(f => f.AircraftModelId == model.Id && f.DepartureDateTime >= start && f.ArrivalDateTime <= end)
            .ToList();

        var tickets = _tickets
            .Where(t => flights.Any(f => f.Id == t.FlightId))
            .ToList();

        var totalPassengers = tickets.Count;
        var totalBaggage = tickets.Sum(t => t.BaggageWeight ?? 0);

        Assert.Equal(2, flights.Count);
        Assert.Equal(7, totalPassengers);
        Assert.Equal(55, totalBaggage);
    }

    /// <summary>
    /// Assignment: Display all flights departing from a specified departure point to a specified arrival point.
    /// </summary>
    [Fact]
    public void FlightsFromMoscowToBerlin()
    {
        var expectedCodes = new[] { "FL001", "FL004" };

        var query = _flights
            .Where(f => f.Departure == "Moscow" && f.Arrival == "Berlin")
            .OrderBy(f => f.Code)
            .ToList();

        Assert.Equal(expectedCodes, query.Select(f => f.Code).ToArray());
    }
}
