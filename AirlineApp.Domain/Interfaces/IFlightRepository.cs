using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing Flight entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface IFlightRepository : IGenericRepository<Flight> { }
