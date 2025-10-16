using AirlineApp.Application.Dtos.TicketDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _repository;
    private readonly IFlightRepository _flightRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IMapper _mapper;

    public TicketController(
        ITicketRepository repository,
        IFlightRepository flightRepository,
        IPassengerRepository passengerRepository,
        IMapper mapper)
    {
        _repository = repository;
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketGetDto>>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<TicketGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketGetDto>> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(_mapper.Map<TicketGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(TicketEditDto dto)
    {
        var flight = await _flightRepository.GetByIdAsync(dto.FlightId);
        if (flight == null) return BadRequest($"Flight with Id {dto.FlightId} not found");

        var passenger = await _passengerRepository.GetByIdAsync(dto.PassengerId);
        if (passenger == null) return BadRequest($"Passenger with Id {dto.PassengerId} not found");

        var ticket = new Ticket
        {
            Flight = flight,
            Passenger = passenger,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight
        };

        await _repository.AddAsync(ticket);
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, _mapper.Map<TicketGetDto>(ticket));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, TicketEditDto dto)
    {
        if (!await _repository.ExistsByIdAsync(id)) return NotFound();

        var flight = await _flightRepository.GetByIdAsync(dto.FlightId);
        if (flight == null) return BadRequest($"Flight with Id {dto.FlightId} not found");

        var passenger = await _passengerRepository.GetByIdAsync(dto.PassengerId);
        if (passenger == null) return BadRequest($"Passenger with Id {dto.PassengerId} not found");

        var ticket = new Ticket
        {
            Id = id,
            Flight = flight,
            Passenger = passenger,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight
        };

        await _repository.UpdateAsync(ticket);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await _repository.ExistsByIdAsync(id)) return NotFound();
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
