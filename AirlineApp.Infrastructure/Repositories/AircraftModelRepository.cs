using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

public class AircraftModelRepository(AppDbContext context) : IAircraftModelRepository
{
    public async Task<IEnumerable<AircraftModel>> GetAllAsync() =>
        await context.AircraftModels.Include(m => m.Family).ToListAsync();

    public async Task<AircraftModel?> GetByIdAsync(int id) =>
        await context.AircraftModels.Include(m => m.Family)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.AircraftModels.AnyAsync(x => x.Id == id);

    public async Task AddAsync(AircraftModel model)
    {
        await context.AircraftModels.AddAsync(model);
        await context.SaveChangesAsync();
    }

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

    public async Task DeleteAsync(int id)
    {
        var entity = await context.AircraftModels.FindAsync(id)
            ?? throw new KeyNotFoundException($"Model with Id {id} not found.");

        context.AircraftModels.Remove(entity);
        await context.SaveChangesAsync();
    }
}