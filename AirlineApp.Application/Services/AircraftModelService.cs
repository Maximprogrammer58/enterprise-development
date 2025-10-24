using AirlineApp.Contracts.Dtos.AircraftModelDtos;
using AirlineApp.Contracts.Interfaces;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

/// <summary>
/// Service for managing aircraft models.
/// </summary>
public class AircraftModelService(IAircraftModelRepository modelRepository,
        IAircraftFamilyRepository familyRepository,
        IMapper mapper) : ICrudService<AircraftModelGetDto, AircraftModelEditDto>
{
    /// <summary>Gets all aircraft models.</summary>
    public async Task<IEnumerable<AircraftModelGetDto>> GetAllAsync()
    {
        var models = await modelRepository.GetAllAsync();
        return mapper.Map<IEnumerable<AircraftModelGetDto>>(models);
    }

    /// <summary>Gets a single aircraft model by ID.</summary>
    public async Task<AircraftModelGetDto?> GetByIdAsync(int id)
    {
        var model = await modelRepository.GetByIdAsync(id);
        return model == null ? null : mapper.Map<AircraftModelGetDto>(model);
    }

    /// <summary>Creates a new aircraft model.</summary>
    public async Task<AircraftModelGetDto> CreateAsync(AircraftModelEditDto dto)
    {
        var family = await familyRepository.GetByIdAsync(dto.FamilyId)
             ?? throw new InvalidOperationException($"AircraftFamily with ID {dto.FamilyId} not found.");

        var model = new AircraftModel
        {
            Name = dto.Name,
            FlightRange = dto.FlightRange,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            Family = family
        };
        await modelRepository.AddAsync(model);
        return mapper.Map<AircraftModelGetDto>(model);
    }

    /// <summary>Updates an existing aircraft model.</summary>
    public async Task UpdateAsync(int id, AircraftModelEditDto dto)
    {
        if (!await modelRepository.ExistsByIdAsync(id))
            throw new InvalidOperationException($"AircraftModel with Id {id} not found.");

        var family = await familyRepository.GetByIdAsync(dto.FamilyId)
             ?? throw new KeyNotFoundException($"AircraftFamily with ID {dto.FamilyId} not found.");

        var model = new AircraftModel
        {
            Id = id,
            Name = dto.Name,
            FlightRange = dto.FlightRange,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            Family = family
        };
        await modelRepository.UpdateAsync(model);
    }

    /// <summary>Deletes an aircraft model.</summary>
    public async Task DeleteAsync(int id)
    {
        var model = await modelRepository.GetByIdWithFlightsAsync(id)
                    ?? throw new InvalidOperationException($"AircraftModel with Id {id} not found");

        if (model.Flights.Any())
            throw new InvalidOperationException("Cannot delete AircraftModel: there are existing Flights linked to it.");

        await modelRepository.DeleteAsync(id);
    }

}
