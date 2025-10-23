using AirlineApp.Contracts.Dtos.FlightDtos;
using AirlineApp.Contracts.Interfaces;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

/// <summary>
/// Service for managing flights.
/// </summary>
public class FlightService(IFlightRepository flightRepository,
        IAircraftModelRepository modelRepository,
        IMapper mapper) : ICrudService<FlightGetDto, FlightEditDto>
{
    /// <summary>Gets all flights.</summary>
    public async Task<IEnumerable<FlightGetDto>> GetAllAsync()
    {
        var flights = await flightRepository.GetAllAsync();
        return mapper.Map<IEnumerable<FlightGetDto>>(flights);
    }

    /// <summary>Gets a single flight by ID.</summary>
    public async Task<FlightGetDto?> GetByIdAsync(int id)
    {
        var flight = await flightRepository.GetByIdAsync(id);
        return flight == null ? null : mapper.Map<FlightGetDto>(flight);
    }

    /// <summary>Creates a new flight.</summary>
    public async Task<FlightGetDto> CreateAsync(FlightEditDto dto)
    {
        var model = await modelRepository.GetByIdAsync(dto.AircraftModelId)
            ?? throw new InvalidOperationException($"AircraftModel with Id {dto.AircraftModelId} not found");

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

        await flightRepository.AddAsync(flight);
        return mapper.Map<FlightGetDto>(flight);
    }

    /// <summary>Updates an existing flight.</summary>
    public async Task UpdateAsync(int id, FlightEditDto dto)
    {
        if (!await flightRepository.ExistsByIdAsync(id))
            throw new InvalidOperationException($"Flight with Id {id} not found");

        var model = await modelRepository.GetByIdAsync(dto.AircraftModelId)
            ?? throw new KeyNotFoundException($"AircraftModel with Id {dto.AircraftModelId} not found");

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

        await flightRepository.UpdateAsync(flight);
    }

    /// <summary>Deletes a flight.</summary>
    public async Task DeleteAsync(int id)
    {
        if (!await flightRepository.ExistsByIdAsync(id))
            throw new InvalidOperationException($"Flight with Id {id} not found");

        await flightRepository.DeleteAsync(id);
    }
}
