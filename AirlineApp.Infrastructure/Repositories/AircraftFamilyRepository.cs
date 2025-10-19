using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="AircraftFamily"/> entities.
/// Provides methods for CRUD operations.
/// </summary>
public class AircraftFamilyRepository(AppDbContext context) : IAircraftFamilyRepository
{
    /// <summary>
    /// Retrieves all aircraft families from the database.
    /// </summary>
    public async Task<IEnumerable<AircraftFamily>> GetAllAsync() =>
        await context.AircraftFamilies.ToListAsync();

    /// <summary>
    /// Retrieves an aircraft family by its ID.
    /// </summary>
    public async Task<AircraftFamily?> GetByIdAsync(int id) =>
        await context.AircraftFamilies.FindAsync(id);

    /// <summary>
    /// Checks whether an aircraft family with the given ID exists.
    /// </summary>
    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.AircraftFamilies.AnyAsync(x => x.Id == id);

    /// <summary>
    /// Adds a new aircraft family to the database.
    /// </summary>
    public async Task AddAsync(AircraftFamily family)
    {
        await context.AircraftFamilies.AddAsync(family);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing aircraft family in the database.
    /// </summary>
    public async Task UpdateAsync(AircraftFamily family)
    {
        var existing = await context.AircraftFamilies.FindAsync(family.Id)
            ?? throw new KeyNotFoundException($"Family with Id {family.Id} not found.");

        existing.Name = family.Name;
        existing.Manufacturer = family.Manufacturer;

        context.AircraftFamilies.Update(existing);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes an aircraft family by its ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await context.AircraftFamilies.FindAsync(id)
            ?? throw new KeyNotFoundException($"Family with Id {id} not found.");

        context.AircraftFamilies.Remove(entity);
        await context.SaveChangesAsync();
    }
}
