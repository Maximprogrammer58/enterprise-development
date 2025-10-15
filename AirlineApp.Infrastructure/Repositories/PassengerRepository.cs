using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

public class PassengerRepository(AppDbContext context) : IPassengerRepository
{
    public async Task<IEnumerable<Passenger>> GetAllAsync() =>
        await context.Passengers.ToListAsync();

    public async Task<Passenger?> GetByIdAsync(int id) =>
        await context.Passengers.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.Passengers.AnyAsync(p => p.Id == id);

    public async Task AddAsync(Passenger passenger)
    {
        await context.Passengers.AddAsync(passenger);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Passenger passenger)
    {
        var existing = await context.Passengers.FindAsync(passenger.Id)
            ?? throw new KeyNotFoundException($"Passenger with Id {passenger.Id} not found.");

        existing.FullName = passenger.FullName;
        existing.PassportNumber = passenger.PassportNumber;
        existing.BirthDate = passenger.BirthDate;

        context.Passengers.Update(existing);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.Passengers.FindAsync(id)
            ?? throw new KeyNotFoundException($"Passenger with Id {id} not found.");

        context.Passengers.Remove(entity);
        await context.SaveChangesAsync();
    }
}
