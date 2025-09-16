using AirlineApp.Tests.Seed;
using System.Data;

namespace AirlineApp.Tests;

/// <summary>
/// Contains unit tests for querying flights, passengers, and tickets.
/// </summary>
public class QueriesTests(DataSeed seed) : IClassFixture<DataSeed>
{
    private readonly DataSeed _seed = seed;

    /// <summary>
    /// Tests that the top 5 flights by passenger count are returned correctly.
    /// </summary>
    [Fact]
    public void TopFlightsByPassengerCount()
    {
        var expected = new[] { ("FL001", 6), ("FL003", 5), ("FL002", 4), ("FL004", 3), ("FL005", 2) };

        var query = _seed.Tickets
            .GroupBy(t => t.Flight)
            .Select(g => new { Flight = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToArray();

        Assert.Equal(expected.Length, query.Length);

        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i].Item1, query[i].Flight.Code);
            Assert.Equal(expected[i].Item2, query[i].Count);
        }
    }

    /// <summary>
    /// Tests that flights with the minimal duration are returned correctly.
    /// </summary>
    [Fact]
    public void FlightsWithMinimalDuration()
    {
        var expectedCodes = new[] { "FL005", "FL009" };

        var minDuration = _seed.Flights
            .Where(f => f.Duration.HasValue)       
            .Min(f => f.Duration!.Value);

        var queryCodes = _seed.Flights
            .Where(f => f.Duration.HasValue && f.Duration.Value == minDuration)
            .OrderBy(f => f.Code)
            .Select(f => f.Code)
            .ToArray();

        Assert.Equal(expectedCodes, queryCodes);
    }

    /// <summary>
    /// Tests that passengers with zero baggage on a specific flight are returned correctly.
    /// </summary>
    [Fact]
    public void PassengersWithZeroBaggage()
    {
        var expected = new[] { "Boris B", "Kirill K" };

        var flight = _seed.Flights.Single(f => f.Code == "FL001");

        var passengerNames = _seed.Tickets
            .Where(t => t.Flight == flight && (t.BaggageWeight ?? 0) == 0)
            .Select(t => t.Passenger.FullName) 
            .OrderBy(name => name)
            .ToArray();

        Assert.Equal(expected, passengerNames);
    }

    /// <summary>
    /// Tests that summary information about a model's flights in a period is calculated correctly.
    /// </summary>
    [Fact]
    public void SummaryInfoModelInPeriod()
    {
        var model = _seed.AircraftModels.Single(m => m.Name == "A320");

        var start = new DateTime(2025, 9, 1);
        var end = start.AddDays(1);

        var flights = _seed.Flights
            .Where(f => f.AircraftModel == model &&
                        f.DepartureDateTime.HasValue && f.ArrivalDateTime.HasValue &&
                        f.DepartureDateTime.Value >= start && f.ArrivalDateTime.Value <= end)
            .ToArray();

        var tickets = _seed.Tickets
            .Where(t => flights.Contains(t.Flight))
            .ToArray();

        var totalPassengers = tickets.Length;
        var totalBaggage = tickets.Sum(t => t.BaggageWeight ?? 0);

        Assert.Equal(2, flights.Length);
        Assert.Equal(7, totalPassengers);
        Assert.Equal(55, totalBaggage);
    }

    /// <summary>
    /// Tests that flights from Moscow to Berlin are returned correctly.
    /// </summary>
    [Fact]
    public void FlightsFromMoscowToBerlin()
    {
        var expectedCodes = new[] { "FL001", "FL004" };

        var flightCodes = _seed.Flights
            .Where(f => f.Departure == "Moscow" && f.Arrival == "Berlin")
            .OrderBy(f => f.Code)
            .Select(f => f.Code)
            .ToArray();

        Assert.Equal(expectedCodes, flightCodes);
    }
}