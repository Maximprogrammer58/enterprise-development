using AirlineApp.Application.Dtos.AircraftFamilyDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

public class AircraftFamilyService
{
    private readonly IAircraftFamilyRepository _repository;
    private readonly IAircraftModelRepository _modelRepository;
    private readonly IMapper _mapper;

    public AircraftFamilyService(
        IAircraftFamilyRepository repository,
        IAircraftModelRepository modelRepository,
        IMapper mapper)
    {
        _repository = repository;
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AircraftFamilyGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AircraftFamilyGetDto>>(entities);
    }

    public async Task<AircraftFamilyGetDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<AircraftFamilyGetDto>(entity);
    }

    public async Task<AircraftFamilyGetDto> CreateAsync(AircraftFamilyEditDto dto)
    {
        var entity = _mapper.Map<AircraftFamily>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<AircraftFamilyGetDto>(entity);
    }

    public async Task<bool> UpdateAsync(int id, AircraftFamilyEditDto dto)
    {
        if (!await _repository.ExistsByIdAsync(id))
            return false;

        var entity = _mapper.Map<AircraftFamily>(dto);
        entity.Id = id;
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id))
            return (false, "AircraftFamily not found");

        // Проверяем, есть ли связанные модели
        var models = await _modelRepository.GetAllAsync();
        if (models.Any(m => m.Family.Id == id))
        {
            return (false, "Cannot delete AircraftFamily: there are existing AircraftModels linked to it.");
        }

        await _repository.DeleteAsync(id);
        return (true, null);
    }
}
