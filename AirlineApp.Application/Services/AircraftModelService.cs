using AirlineApp.Application.Dtos.AircraftModelDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

public class AircraftModelService
{
    private readonly IAircraftModelRepository _modelRepository;
    private readonly IAircraftFamilyRepository _familyRepository;
    private readonly IFlightRepository _flightRepository;
    private readonly IMapper _mapper;

    public AircraftModelService(
        IAircraftModelRepository modelRepository,
        IAircraftFamilyRepository familyRepository,
        IFlightRepository flightRepository,
        IMapper mapper)
    {
        _modelRepository = modelRepository;
        _familyRepository = familyRepository;
        _flightRepository = flightRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AircraftModelGetDto>> GetAllAsync()
    {
        var models = await _modelRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AircraftModelGetDto>>(models);
    }

    public async Task<AircraftModelGetDto?> GetByIdAsync(int id)
    {
        var model = await _modelRepository.GetByIdAsync(id);
        return model == null ? null : _mapper.Map<AircraftModelGetDto>(model);
    }

    public async Task<(bool Success, AircraftModelGetDto? Result, string? Error)> CreateAsync(AircraftModelEditDto dto)
    {
        var family = await _familyRepository.GetByIdAsync(dto.FamilyId);
        if (family == null) return (false, null, $"AircraftFamily with Id {dto.FamilyId} not found");

        var model = new AircraftModel
        {
            Name = dto.Name,
            FlightRange = dto.FlightRange,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            Family = family
        };

        await _modelRepository.AddAsync(model);
        return (true, _mapper.Map<AircraftModelGetDto>(model), null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(int id, AircraftModelEditDto dto)
    {
        if (!await _modelRepository.ExistsByIdAsync(id)) return (false, "Model not found");

        var family = await _familyRepository.GetByIdAsync(dto.FamilyId);
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

        await _modelRepository.UpdateAsync(model);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(int id)
    {
        if (!await _modelRepository.ExistsByIdAsync(id))
            return (false, "AircraftModel not found");

        // Проверка зависимых рейсов
        var flights = await _flightRepository.GetAllAsync();
        if (flights.Any(f => f.AircraftModel.Id == id))
            return (false, "Cannot delete AircraftModel: there are existing Flights linked to it.");

        await _modelRepository.DeleteAsync(id);
        return (true, null);
    }
}
