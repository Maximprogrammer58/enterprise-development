using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

public interface IPassengerRepository
{
    public Task<IEnumerable<Passenger>> GetAllAsync();
    public Task<Passenger?> GetByIdAsync(int id);
    public Task<bool> ExistsByIdAsync(int id);
    public Task AddAsync(Passenger passenger);
    public Task UpdateAsync(Passenger passenger);
    public Task DeleteAsync(int id);
}