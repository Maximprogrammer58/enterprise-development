using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

public class FlightRepository(AppDbContext context) : IFlightRepository
{
    public async Task<IEnumerable<Flight>> GetAllAsync() =>
        await context.Flights.Include(f => f.AircraftModel).ToListAsync();

    public async Task<Flight?> GetByIdAsync(int id) =>
        await context.Flights.Include(f => f.AircraftModel)
            .FirstOrDefaultAsync(f => f.Id == id);
    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.Flights.AnyAsync(f => f.Id == id);

    public async Task AddAsync(Flight flight)
    {
        await context.Flights.AddAsync(flight);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Flight flight)
    {
        var existing = await context.Flights.FindAsync(flight.Id)
            ?? throw new KeyNotFoundException($"Flight with Id {flight.Id} not found.");

        existing.Code = flight.Code;
        existing.Departure = flight.Departure;
        existing.Arrival = flight.Arrival;
        existing.DepartureDateTime = flight.DepartureDateTime;
        existing.ArrivalDateTime = flight.ArrivalDateTime;
        existing.Duration = flight.Duration;
        existing.AircraftModel = flight.AircraftModel;

        context.Flights.Update(existing);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.Flights.FindAsync(id)
            ?? throw new KeyNotFoundException($"Flight with Id {id} not found.");

        context.Flights.Remove(entity);
        await context.SaveChangesAsync();
    }
}