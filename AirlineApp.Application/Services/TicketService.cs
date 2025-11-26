using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Contracts.Interfaces;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AirlineApp.Application.Services;

/// <summary>
/// Service for managing tickets.
/// </summary>
public class TicketService(ITicketRepository ticketRepository,
        IFlightRepository flightRepository,
        IPassengerRepository passengerRepository,
        IMapper mapper,
        ILogger<TicketService> logger) : ITicketService
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

    /// <summary>
    /// Processes a batch of ticket contracts received from message queue.
    /// Creates tickets in bulk after validating flight and passenger references.
    /// </summary>
    /// <param name="contracts">List of ticket contracts to process</param>
    public async Task ReceiveContractList(IList<TicketEditDto> contracts)
    {
        var processedCount = 0;
        var errorCount = 0;

        foreach (var dto in contracts)
        {
            try
            {
                var flight = await flightRepository.GetByIdAsync(dto.FlightId)
                    ?? throw new KeyNotFoundException($"Flight with Id {dto.FlightId} not found");

                var passenger = await passengerRepository.GetByIdAsync(dto.PassengerId)
                    ?? throw new KeyNotFoundException($"Passenger with Id {dto.PassengerId} not found");

                if (await ticketRepository.ExistsByFlightAndSeatAsync(dto.FlightId, dto.SeatNumber))
                    throw new InvalidOperationException($"Seat {dto.SeatNumber} is already occupied in flight {dto.FlightId}");

                if (await ticketRepository.ExistsByFlightAndPassengerAsync(dto.FlightId, dto.PassengerId))
                    throw new InvalidOperationException($"Passenger {dto.PassengerId} already has a ticket for flight {dto.FlightId}");

                var ticket = new Ticket
                {
                    Flight = flight,
                    Passenger = passenger,
                    SeatNumber = dto.SeatNumber,
                    HasHandLuggage = dto.HasHandLuggage,
                    BaggageWeight = dto.BaggageWeight
                };

                await ticketRepository.AddAsync(ticket);
                processedCount++;

                logger.LogInformation("Successfully processed ticket for flight {FlightId}, passenger {PassengerId}, seat {SeatNumber}",
                    dto.FlightId, dto.PassengerId, dto.SeatNumber);
            }
            catch (Exception ex)
            {
                errorCount++;
                logger.LogWarning(ex, "Failed to process contract for flight {FlightId}, passenger {PassengerId}",
                    dto.FlightId, dto.PassengerId);
            }
        }

        logger.LogInformation("Ticket processing completed: {ProcessedCount} successful, {ErrorCount} failed out of {TotalCount}",
            processedCount, errorCount, contracts.Count);
    }
}
