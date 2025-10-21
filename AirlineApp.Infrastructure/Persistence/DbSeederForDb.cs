using AirlineApp.Tests.Seed;

namespace AirlineApp.Infrastructure.Persistence;

/// <summary>
/// Seeds the database with initial demo data.
/// Can optionally force reset all data.
/// </summary>
public class DbSeederForDb(AppDbContext context)
{
    /// <summary>
    /// Seeds demo data into the database.
    /// </summary>
    /// <param name="forceReset">If true, clears existing data before seeding.</param>
    public async Task SeedAsync(bool forceReset = false)
    {
        if (forceReset)
        {
            context.Tickets.RemoveRange(context.Tickets);
            context.Flights.RemoveRange(context.Flights);
            context.Passengers.RemoveRange(context.Passengers);
            context.AircraftModels.RemoveRange(context.AircraftModels);
            context.AircraftFamilies.RemoveRange(context.AircraftFamilies);
            await context.SaveChangesAsync();
        }

        if (!forceReset && (
            context.AircraftFamilies.Any() ||
            context.AircraftModels.Any() ||
            context.Passengers.Any() ||
            context.Flights.Any() ||
            context.Tickets.Any()))
        {
            return;
        }

        var seed = new DataSeed();

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
