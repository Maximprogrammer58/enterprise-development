using AirlineApp.Domain.Entities;

namespace AirlineApp.Domain.Interfaces;

public interface IAircraftFamilyRepository
{
    public Task<IEnumerable<AircraftFamily>> GetAllAsync();
    public Task<AircraftFamily?> GetByIdAsync(int id);
    public Task<bool> ExistsByIdAsync(int id);
    public Task AddAsync(AircraftFamily family);
    public Task UpdateAsync(AircraftFamily family);
    public Task DeleteAsync(int id);
}
