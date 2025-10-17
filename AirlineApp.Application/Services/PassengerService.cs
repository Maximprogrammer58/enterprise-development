using AirlineApp.Application.Dtos.PassengerDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

public class PassengerService
{
    private readonly IPassengerRepository _repository;
    private readonly IMapper _mapper;

    public PassengerService(IPassengerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PassengerGetDto>> GetAllAsync()
    {
        var passengers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<PassengerGetDto>>(passengers);
    }

    public async Task<PassengerGetDto?> GetByIdAsync(int id)
    {
        var passenger = await _repository.GetByIdAsync(id);
        return passenger == null ? null : _mapper.Map<PassengerGetDto>(passenger);
    }

    public async Task<PassengerGetDto> CreateAsync(PassengerEditDto dto)
    {
        var passenger = _mapper.Map<Passenger>(dto);
        await _repository.AddAsync(passenger);
        return _mapper.Map<PassengerGetDto>(passenger);
    }

    public async Task<bool> UpdateAsync(int id, PassengerEditDto dto)
    {
        if (!await _repository.ExistsByIdAsync(id)) return false;
        var passenger = _mapper.Map<Passenger>(dto);
        passenger.Id = id;
        await _repository.UpdateAsync(passenger);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _repository.ExistsByIdAsync(id)) return false;
        await _repository.DeleteAsync(id);
        return true;
    }
}
