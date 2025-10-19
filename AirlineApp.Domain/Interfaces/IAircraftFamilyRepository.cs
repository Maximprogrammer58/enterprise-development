using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing AircraftFamily entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface IAircraftFamilyRepository : IGenericRepository<AircraftFamily> { }