using AirlineApp.Application.Dtos.PassengerDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

/// <summary>
/// Service for managing passengers.
/// </summary>
public class PassengerService(IPassengerRepository repository, IMapper mapper)
{
    /// <summary>Gets all passengers.</summary>
    public async Task<IEnumerable<PassengerGetDto>> GetAllAsync()
    {
        var passengers = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<PassengerGetDto>>(passengers);
    }

    /// <summary>Gets a single passenger by ID.</summary>
    public async Task<PassengerGetDto?> GetByIdAsync(int id)
    {
        var passenger = await repository.GetByIdAsync(id);
        return passenger == null ? null : mapper.Map<PassengerGetDto>(passenger);
    }

    ///  /// <summary>Creates a new passenger.</summary>
    public async Task<PassengerGetDto> CreateAsync(PassengerEditDto dto)
    {
        var passenger = mapper.Map<Passenger>(dto);
        await repository.AddAsync(passenger);
        return mapper.Map<PassengerGetDto>(passenger);
    }

    /// <summary>Updates an existing passenger.</summary>
    public async Task UpdateAsync(int id, PassengerEditDto dto)
    {
        var passenger = mapper.Map<Passenger>(dto);
        passenger.Id = id;
        await repository.UpdateAsync(passenger);
    }


    /// <summary>Deletes a passenger.</summary>
    public async Task DeleteAsync(int id) =>
        await repository.DeleteAsync(id);
}
