namespace AirlineApp.Contracts.Interfaces;

public interface ICrudService<TEntityDto, TEditDto>
{
    public Task<IEnumerable<TEntityDto>> GetAllAsync();
    public Task<TEntityDto?> GetByIdAsync(int id);
    public Task<TEntityDto> CreateAsync(TEditDto dto);
    public Task UpdateAsync(int id, TEditDto dto);
    public Task DeleteAsync(int id);
}
