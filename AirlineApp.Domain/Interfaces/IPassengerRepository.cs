using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing Passenger entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface IPassengerRepository : IGenericRepository<Passenger> { }
