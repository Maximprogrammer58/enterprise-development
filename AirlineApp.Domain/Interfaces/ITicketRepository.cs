using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

/// <summary>
/// Repository for managing Ticket entities.
/// Provides CRUD operations and existence checks.
/// </summary>
public interface ITicketRepository : IGenericRepository<Ticket>
{
    /// <summary>
    /// Checks if a seat is already occupied in a specific flight.
    /// </summary>
    /// <param name="flightId">The flight identifier</param>
    /// <param name="seatNumber">The seat number to check</param>
    /// <returns>True if the seat is occupied; otherwise, false</returns>
    public Task<bool> ExistsByFlightAndSeatAsync(int flightId, string seatNumber);

    /// <summary>
    /// Checks if a passenger already has a ticket for a specific flight.
    /// </summary>
    /// <param name="flightId">The flight identifier</param>
    /// <param name="passengerId">The passenger identifier</param>
    /// <returns>True if the passenger already has a ticket for this flight; otherwise, false</returns>
    public Task<bool> ExistsByFlightAndPassengerAsync(int flightId, int passengerId);
}
