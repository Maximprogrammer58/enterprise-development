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
        var existing = await context.Tickets.FindAsync(ticket.Id)
            ?? throw new KeyNotFoundException($"Ticket with Id {ticket.Id} not found.");
        existing.Flight = ticket.Flight;
        existing.Passenger = ticket.Passenger;
        existing.SeatNumber = ticket.SeatNumber;
        existing.HasHandLuggage = ticket.HasHandLuggage;
        existing.BaggageWeight = ticket.BaggageWeight;

        context.Tickets.Update(existing);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a ticket by ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var entity = await context.Tickets.FindAsync(id)
            ?? throw new KeyNotFoundException($"Ticket with Id {id} not found.");

        context.Tickets.Remove(entity);
        await context.SaveChangesAsync();
    }
}
