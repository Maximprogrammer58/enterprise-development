using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

public class AircraftFamilyRepository(AppDbContext context) : IAircraftFamilyRepository
{
    public async Task<IEnumerable<AircraftFamily>> GetAllAsync() =>
        await context.AircraftFamilies.ToListAsync();

    public async Task<AircraftFamily?> GetByIdAsync(int id) =>
        await context.AircraftFamilies.FindAsync(id);

    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.AircraftFamilies.AnyAsync(x => x.Id == id);

    public async Task AddAsync(AircraftFamily family)
    {
        await context.AircraftFamilies.AddAsync(family);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AircraftFamily family)
    {
        var existing = await context.AircraftFamilies.FindAsync(family.Id)
            ?? throw new KeyNotFoundException($"Family with Id {family.Id} not found.");

        existing.Name = family.Name;
        existing.Manufacturer = family.Manufacturer;

        context.AircraftFamilies.Update(existing);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.AircraftFamilies.FindAsync(id)
            ?? throw new KeyNotFoundException($"Family with Id {id} not found.");

        context.AircraftFamilies.Remove(entity);
        await context.SaveChangesAsync();
    }
}
