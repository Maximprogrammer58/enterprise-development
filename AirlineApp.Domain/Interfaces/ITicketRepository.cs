using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

public interface ITicketRepository
{
    public Task<IEnumerable<Ticket>> GetAllAsync();
    public Task<Ticket?> GetByIdAsync(int id);
    public Task<bool> ExistsByIdAsync(int id);
    public Task AddAsync(Ticket ticket);
    public Task UpdateAsync(Ticket ticket);
    public Task DeleteAsync(int id);
}
