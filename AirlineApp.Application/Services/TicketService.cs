using AirlineApp.Application.Dtos.TicketDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

public class TicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IFlightRepository _flightRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IMapper _mapper;

    public TicketService(
        ITicketRepository ticketRepository,
        IFlightRepository flightRepository,
        IPassengerRepository passengerRepository,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TicketGetDto>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TicketGetDto>>(tickets);
    }

    public async Task<TicketGetDto?> GetByIdAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        return ticket == null ? null : _mapper.Map<TicketGetDto>(ticket);
    }

    public async Task<(bool Success, TicketGetDto? Result, string? ErrorMessage)> CreateAsync(TicketEditDto dto)
    {
        var flight = await _flightRepository.GetByIdAsync(dto.FlightId);
        if (flight == null) return (false, null, $"Flight with Id {dto.FlightId} not found");

        var passenger = await _passengerRepository.GetByIdAsync(dto.PassengerId);
        if (passenger == null) return (false, null, $"Passenger with Id {dto.PassengerId} not found");

        var ticket = new Ticket
        {
            Flight = flight,
            Passenger = passenger,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight
        };

        await _ticketRepository.AddAsync(ticket);
        return (true, _mapper.Map<TicketGetDto>(ticket), null);
    }

    public async Task<(bool Success, string? ErrorMessage)> UpdateAsync(int id, TicketEditDto dto)
    {
        if (!await _ticketRepository.ExistsByIdAsync(id)) return (false, null);

        var flight = await _flightRepository.GetByIdAsync(dto.FlightId);
        if (flight == null) return (false, $"Flight with Id {dto.FlightId} not found");

        var passenger = await _passengerRepository.GetByIdAsync(dto.PassengerId);
        if (passenger == null) return (false, $"Passenger with Id {dto.PassengerId} not found");

        var ticket = new Ticket
        {
            Id = id,
            Flight = flight,
            Passenger = passenger,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight
        };

        await _ticketRepository.UpdateAsync(ticket);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _ticketRepository.ExistsByIdAsync(id)) return false;
        await _ticketRepository.DeleteAsync(id);
        return true;
    }
}
