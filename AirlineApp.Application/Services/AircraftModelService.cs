using AirlineApp.Application.Dtos.AircraftModelDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

/// <summary>
/// Service for managing aircraft models.
/// </summary>
public class AircraftModelService(IAircraftModelRepository modelRepository,
        IAircraftFamilyRepository familyRepository,
        IFlightRepository flightRepository,
        IMapper mapper)
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
    public async Task<(bool Success, AircraftModelGetDto? Result, string? Error)> CreateAsync(AircraftModelEditDto dto)
    {
        var family = await familyRepository.GetByIdAsync(dto.FamilyId);
        if (family == null) return (false, null, $"AircraftFamily with Id {dto.FamilyId} not found");

        var model = new AircraftModel
        {
            Name = dto.Name,
            FlightRange = dto.FlightRange,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            Family = family
        };

        await modelRepository.AddAsync(model);
        return (true, mapper.Map<AircraftModelGetDto>(model), null);
    }

    /// <summary>Updates an existing aircraft model.</summary>
    public async Task<(bool Success, string? Error)> UpdateAsync(int id, AircraftModelEditDto dto)
    {
        if (!await modelRepository.ExistsByIdAsync(id)) return (false, "Model not found");

        var family = await familyRepository.GetByIdAsync(dto.FamilyId);
        if (family == null) return (false, $"AircraftFamily with Id {dto.FamilyId} not found");

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
        return (true, null);
    }

    /// <summary>Deletes an aircraft model.</summary>
    public async Task<(bool Success, string? Error)> DeleteAsync(int id)
    {
        if (!await modelRepository.ExistsByIdAsync(id))
            return (false, "AircraftModel not found");

        var flights = await flightRepository.GetAllAsync();
        if (flights.Any(f => f.AircraftModel.Id == id))
            return (false, "Cannot delete AircraftModel: there are existing Flights linked to it.");

        await modelRepository.DeleteAsync(id);
        return (true, null);
    }
}
