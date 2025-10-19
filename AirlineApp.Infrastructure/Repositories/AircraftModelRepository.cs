using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="AircraftModel"/> entities.
/// Provides basic CRUD operations.
/// </summary>
public class AircraftModelRepository(AppDbContext context) : IAircraftModelRepository
{
    /// <summary>
    /// Retrieves all aircraft models including their family information.
    /// </summary>
    public async Task<IEnumerable<AircraftModel>> GetAllAsync() =>
        await context.AircraftModels.Include(m => m.Family).ToListAsync();

    /// <summary>
    /// Retrieves an aircraft model by its ID including family data.
    /// </summary>
    public async Task<AircraftModel?> GetByIdAsync(int id) =>
        await context.AircraftModels.Include(m => m.Family)
            .FirstOrDefaultAsync(m => m.Id == id);

    /// <summary>
    /// Checks if an aircraft model exists by its ID.
    /// </summary>
    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.AircraftModels.AnyAsync(x => x.Id == id);

    /// <summary>
    /// Adds a new aircraft model to the database.
    /// </summary>
    public async Task AddAsync(AircraftModel model)
    {
        await context.AircraftModels.AddAsync(model);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing aircraft model in the database.
    /// </summary>
    public async Task UpdateAsync(AircraftModel model)
    {
        var existing = await context.AircraftModels.FindAsync(model.Id)
            ?? throw new KeyNotFoundException($"Model with Id {model.Id} not found.");

        existing.Name = model.Name;
        existing.FlightRange = model.FlightRange;
        existing.PassengerCapacity = model.PassengerCapacity;
        existing.CargoCapacity = model.CargoCapacity;
        existing.Family = model.Family;

        context.AircraftModels.Update(existing);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes an aircraft model by its ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await context.AircraftModels.FindAsync(id)
            ?? throw new KeyNotFoundException($"Model with Id {id} not found.");

        context.AircraftModels.Remove(entity);
        await context.SaveChangesAsync();
    }
}