using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

public interface IFlightRepository
{
    public Task<IEnumerable<Flight>> GetAllAsync();
    public Task<Flight?> GetByIdAsync(int id);
    public Task<bool> ExistsByIdAsync(int id);
    public Task AddAsync(Flight flight);
    public Task UpdateAsync(Flight flight);
    public Task DeleteAsync(int id);
}
