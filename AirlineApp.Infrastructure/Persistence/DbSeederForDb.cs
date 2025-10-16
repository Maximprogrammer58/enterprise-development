using AirlineApp.Domain.Entities;
using AirlineApp.Tests.Seed;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Persistence;

public class DbSeederForDb
{
    private readonly AppDbContext _context;

    public DbSeederForDb(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        var seed = new DataSeed();

        _context.Tickets.RemoveRange(_context.Tickets);
        _context.Flights.RemoveRange(_context.Flights);
        _context.Passengers.RemoveRange(_context.Passengers);
        _context.AircraftModels.RemoveRange(_context.AircraftModels);
        _context.AircraftFamilies.RemoveRange(_context.AircraftFamilies);
        await _context.SaveChangesAsync();

        await _context.AircraftFamilies.AddRangeAsync(seed.AircraftFamilies);
        await _context.SaveChangesAsync();

        await _context.AircraftModels.AddRangeAsync(seed.AircraftModels);
        await _context.SaveChangesAsync();

        await _context.Passengers.AddRangeAsync(seed.Passengers);
        await _context.SaveChangesAsync();

        await _context.Flights.AddRangeAsync(seed.Flights);
        await _context.SaveChangesAsync();

        await _context.Tickets.AddRangeAsync(seed.Tickets);
        await _context.SaveChangesAsync();
    }
}
