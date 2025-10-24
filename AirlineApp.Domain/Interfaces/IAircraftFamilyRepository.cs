using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing AircraftFamily entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface IAircraftFamilyRepository : IGenericRepository<AircraftFamily> 
{
    /// <summary>
    /// Retrieves an AircraftFamily by its ID, including all associated AircraftModels.
    /// </summary>
    public Task<AircraftFamily?> GetByIdWithModelsAsync(int id);
}