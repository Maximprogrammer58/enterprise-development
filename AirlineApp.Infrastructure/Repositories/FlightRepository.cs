using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="Flight"/> entities.
/// Provides basic CRUD operations.
/// </summary>
public class FlightRepository(AppDbContext context) : IFlightRepository
{
    /// <summary>
    /// Retrieves all flights including their aircraft model information.
    /// </summary>
    public async Task<IEnumerable<Flight>> GetAllAsync() =>
        await context.Flights.Include(f => f.AircraftModel).ToListAsync();

    /// <summary>
    /// Retrieves a flight by its ID including aircraft model data.
    /// </summary>
    public async Task<Flight?> GetByIdAsync(int id) =>
        await context.Flights.Include(f => f.AircraftModel)
            .FirstOrDefaultAsync(f => f.Id == id);

    /// <summary>
    /// Checks if a flight exists by its ID.
    /// </summary>
    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.Flights.AnyAsync(f => f.Id == id);

    /// <summary>
    /// Adds a new flight to the database.
    /// </summary>
    public async Task AddAsync(Flight flight)
    {
        await context.Flights.AddAsync(flight);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing flight in the database.
    /// </summary>
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

    /// <summary>
    /// Deletes a flight by its ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await context.Flights.FindAsync(id)
            ?? throw new KeyNotFoundException($"Flight with Id {id} not found.");

        context.Flights.Remove(entity);
        await context.SaveChangesAsync();
    }
}