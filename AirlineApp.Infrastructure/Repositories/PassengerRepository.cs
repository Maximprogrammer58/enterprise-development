using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="Passenger"/> entities.
/// Provides basic CRUD operations.
/// </summary>
public class PassengerRepository(AppDbContext context) : IPassengerRepository
{
    /// <summary>
    /// Retrieves all passengers.
    /// </summary>
    public async Task<IEnumerable<Passenger>> GetAllAsync() =>
        await context.Passengers.ToListAsync();

    /// <summary>
    /// Retrieves a passenger by ID.
    /// </summary>
    public async Task<Passenger?> GetByIdAsync(int id) =>
        await context.Passengers.FirstOrDefaultAsync(p => p.Id == id);

    /// <summary>
    /// Checks if a passenger exists by ID.
    /// </summary>
    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.Passengers.AnyAsync(p => p.Id == id);

    /// <summary>
    /// Adds a new passenger to the database.
    /// </summary>
    public async Task AddAsync(Passenger passenger)
    {
        await context.Passengers.AddAsync(passenger);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing passenger.
    /// </summary>
    public async Task UpdateAsync(Passenger passenger)
    {
        context.Passengers.Update(passenger);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a passenger by ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await context.Passengers.FindAsync(id)
            ?? throw new KeyNotFoundException($"Passenger with Id {id} not found.");

        context.Passengers.Remove(entity);
        await context.SaveChangesAsync();
    }
}
