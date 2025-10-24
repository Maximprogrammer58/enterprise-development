using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing Flight entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface IFlightRepository : IGenericRepository<Flight> 
{
    /// <summary>
    /// Retrieves an Flight by its ID, including all associated Tickets.
    /// </summary>
    public Task<Flight?> GetByIdWithTicketsAsync(int id);
}
