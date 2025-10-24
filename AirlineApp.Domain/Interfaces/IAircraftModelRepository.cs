using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing AircraftModel entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface IAircraftModelRepository : IGenericRepository<AircraftModel> 
{
    /// <summary>
    /// Retrieves an AircraftModel by its ID, including all associated Flights.
    /// </summary>
    public Task<AircraftModel?> GetByIdWithFlightsAsync(int id);
}
