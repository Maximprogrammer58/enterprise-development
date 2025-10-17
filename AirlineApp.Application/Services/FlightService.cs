using AirlineApp.Application.Dtos.FlightDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

public class FlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAircraftModelRepository _modelRepository;
    private readonly IMapper _mapper;

    public FlightService(
        IFlightRepository flightRepository,
        IAircraftModelRepository modelRepository,
        IMapper mapper)
    {
        _flightRepository = flightRepository;
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FlightGetDto>> GetAllAsync()
    {
        var flights = await _flightRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FlightGetDto>>(flights);
    }

    public async Task<FlightGetDto?> GetByIdAsync(int id)
    {
        var flight = await _flightRepository.GetByIdAsync(id);
        return flight == null ? null : _mapper.Map<FlightGetDto>(flight);
    }

    public async Task<(bool Success, FlightGetDto? Result, string? Error)> CreateAsync(FlightEditDto dto)
    {
        var model = await _modelRepository.GetByIdAsync(dto.AircraftModelId);
        if (model == null) return (false, null, $"AircraftModel with Id {dto.AircraftModelId} not found");

        var flight = new Flight
        {
            Code = dto.Code,
            Departure = dto.Departure,
            Arrival = dto.Arrival,
            DepartureDateTime = dto.DepartureDateTime,
            ArrivalDateTime = dto.ArrivalDateTime,
            Duration = dto.Duration,
            AircraftModel = model
        };

        await _flightRepository.AddAsync(flight);
        return (true, _mapper.Map<FlightGetDto>(flight), null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(int id, FlightEditDto dto)
    {
        if (!await _flightRepository.ExistsByIdAsync(id)) return (false, "Flight not found");

        var model = await _modelRepository.GetByIdAsync(dto.AircraftModelId);
        if (model == null) return (false, $"AircraftModel with Id {dto.AircraftModelId} not found");

        var flight = new Flight
        {
            Id = id,
            Code = dto.Code,
            Departure = dto.Departure,
            Arrival = dto.Arrival,
            DepartureDateTime = dto.DepartureDateTime,
            ArrivalDateTime = dto.ArrivalDateTime,
            Duration = dto.Duration,
            AircraftModel = model
        };

        await _flightRepository.UpdateAsync(flight);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _flightRepository.ExistsByIdAsync(id)) return false;
        await _flightRepository.DeleteAsync(id);
        return true;
    }
}
