using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Contracts.Interfaces;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;

namespace AirlineApp.Application.Services;

/// <summary>
/// Service for managing tickets.
/// </summary>
public class TicketService(ITicketRepository ticketRepository,
        IFlightRepository flightRepository,
        IPassengerRepository passengerRepository,
        IMapper mapper) : ICrudService<TicketGetDto, TicketEditDto>
{
    /// <summary>Gets all tickets.</summary>
    public async Task<IEnumerable<TicketGetDto>> GetAllAsync()
    {
        var tickets = await ticketRepository.GetAllAsync();
        return mapper.Map<IEnumerable<TicketGetDto>>(tickets);
    }

    /// <summary>Gets a single ticket by ID.</summary>
    public async Task<TicketGetDto?> GetByIdAsync(int id)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);
        return ticket == null ? null : mapper.Map<TicketGetDto>(ticket);
    }

    /// <summary>Creates a new ticket.</summary>
    public async Task<TicketGetDto> CreateAsync(TicketEditDto dto)
    {
        var flight = await flightRepository.GetByIdAsync(dto.FlightId)
                 ?? throw new InvalidOperationException($"Flight with Id {dto.FlightId} not found");

        var passenger = await passengerRepository.GetByIdAsync(dto.PassengerId)
                        ?? throw new InvalidOperationException($"Passenger with Id {dto.PassengerId} not found");

        var ticket = new Ticket
        {
            Flight = flight,
            Passenger = passenger,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight
        };

        await ticketRepository.AddAsync(ticket);
        return mapper.Map<TicketGetDto>(ticket);
    }

    /// <summary>Updates an existing ticket.</summary>
    public async Task UpdateAsync(int id, TicketEditDto dto)
    {
        if (!await ticketRepository.ExistsByIdAsync(id))
            throw new InvalidOperationException($"Ticket with Id {id} not found");

        var flight = await flightRepository.GetByIdAsync(dto.FlightId)
                  ?? throw new KeyNotFoundException($"Flight with Id {dto.FlightId} not found");

        var passenger = await passengerRepository.GetByIdAsync(dto.PassengerId)
                        ?? throw new KeyNotFoundException($"Passenger with Id {dto.PassengerId} not found");

        var ticket = new Ticket
        {
            Id = id,
            Flight = flight,
            Passenger = passenger,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight
        };

        await ticketRepository.UpdateAsync(ticket);
    }

    /// <summary>Deletes a ticket.</summary>
    public async Task DeleteAsync(int id)
    {
        if (!await ticketRepository.ExistsByIdAsync(id))
            throw new InvalidOperationException($"Ticket with Id {id} not found");

        await ticketRepository.DeleteAsync(id);
    }
}
