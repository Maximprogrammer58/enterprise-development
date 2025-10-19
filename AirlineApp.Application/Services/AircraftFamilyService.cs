using AirlineApp.Application.Dtos.AircraftFamilyDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

/// <summary>
/// Service for managing aircraft families.
/// </summary>
public class AircraftFamilyService(IAircraftFamilyRepository repository,
        IAircraftModelRepository modelRepository,
        IMapper mapper)
{
    /// <summary>Gets all aircraft families.</summary>
    public async Task<IEnumerable<AircraftFamilyGetDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<AircraftFamilyGetDto>>(entities);
    }

    /// <summary>Gets a single aircraft family by ID.</summary>
    public async Task<AircraftFamilyGetDto?> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity == null ? null : mapper.Map<AircraftFamilyGetDto>(entity);
    }

    /// <summary>Creates a new aircraft family.</summary>
    public async Task<AircraftFamilyGetDto> CreateAsync(AircraftFamilyEditDto dto)
    {
        var entity = mapper.Map<AircraftFamily>(dto);
        await repository.AddAsync(entity);
        return mapper.Map<AircraftFamilyGetDto>(entity);
    }

    /// <summary>Updates an existing aircraft family.</summary>
    public async Task<bool> UpdateAsync(int id, AircraftFamilyEditDto dto)
    {
        if (!await repository.ExistsByIdAsync(id))
            return false;

        var entity = mapper.Map<AircraftFamily>(dto);
        entity.Id = id;
        await repository.UpdateAsync(entity);
        return true;
    }

    /// <summary>Deletes an aircraft family.</summary>
    public async Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
            return (false, "AircraftFamily not found");

        var models = await modelRepository.GetAllAsync();
        if (models.Any(m => m.Family.Id == id))
        {
            return (false, "Cannot delete AircraftFamily: there are existing AircraftModels linked to it.");
        }

        await repository.DeleteAsync(id);
        return (true, null);
    }
}
