using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

public interface IAircraftModelRepository
{
    public Task<IEnumerable<AircraftModel>> GetAllAsync();
    public Task<AircraftModel?> GetByIdAsync(int id);
    public Task<bool> ExistsByIdAsync(int id);
    public Task AddAsync(AircraftModel model);
    public Task UpdateAsync(AircraftModel model);
    public Task DeleteAsync(int id);
}
