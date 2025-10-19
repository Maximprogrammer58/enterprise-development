using AirlineApp.Tests.Seed;

namespace AirlineApp.Infrastructure.Persistence;

/// <summary>
/// Provides methods to seed the database with initial data for testing or development.
/// </summary>
public class DbSeederForDb(AppDbContext context)
{
    public async Task SeedAsync()
    {
        var seed = new DataSeed();

        context.Tickets.RemoveRange(context.Tickets);
        context.Flights.RemoveRange(context.Flights);
        context.Passengers.RemoveRange(context.Passengers);
        context.AircraftModels.RemoveRange(context.AircraftModels);
        context.AircraftFamilies.RemoveRange(context.AircraftFamilies);
        await context.SaveChangesAsync();

        await context.AircraftFamilies.AddRangeAsync(seed.AircraftFamilies);
        await context.SaveChangesAsync();

        await context.AircraftModels.AddRangeAsync(seed.AircraftModels);
        await context.SaveChangesAsync();

        await context.Passengers.AddRangeAsync(seed.Passengers);
        await context.SaveChangesAsync();

        await context.Flights.AddRangeAsync(seed.Flights);
        await context.SaveChangesAsync();

        await context.Tickets.AddRangeAsync(seed.Tickets);
        await context.SaveChangesAsync();
    }
}
