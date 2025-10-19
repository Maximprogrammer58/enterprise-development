using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing Ticket entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface ITicketRepository : IGenericRepository<Ticket> { }
