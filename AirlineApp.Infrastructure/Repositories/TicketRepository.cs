using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="Ticket"/> entities.
/// Provides basic CRUD operations.
/// </summary>
public class TicketRepository(AppDbContext context) : ITicketRepository
{
    /// <summary>
    /// Retrieves all tickets including their related flights and passengers.
    /// </summary>
    public async Task<IEnumerable<Ticket>> GetAllAsync() =>
        await context.Tickets.Include(t => t.Flight).ThenInclude(f => f.AircraftModel)
                             .Include(t => t.Passenger)
                             .ToListAsync();

    /// <summary>
    /// Retrieves a ticket by ID including its related flight and passenger.
    /// </summary>
    public async Task<Ticket?> GetByIdAsync(int id) =>
        await context.Tickets.Include(t => t.Flight).ThenInclude(f => f.AircraftModel)
                             .Include(t => t.Passenger)
                             .FirstOrDefaultAsync(t => t.Id == id);

    /// <summary>
    /// Checks if a ticket exists by ID.
    /// </summary>
    public async Task<bool> ExistsByIdAsync(int id) =>
        await context.Tickets.AnyAsync(t => t.Id == id);

    /// <summary>
    /// Adds a new ticket to the database.
    /// </summary>
    public async Task AddAsync(Ticket ticket)
    {
        await context.Tickets.AddAsync(ticket);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing ticket.
    /// </summary>
    public async Task UpdateAsync(Ticket ticket)
    {
        context.Tickets.Update(ticket);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a ticket by ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await context.Tickets.FindAsync(id);
        if (entity == null) return;

        context.Tickets.Remove(entity);
        await context.SaveChangesAsync();
    }
}
