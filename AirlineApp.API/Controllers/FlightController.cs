using AirlineApp.Application.Dtos.FlightDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/flights")]
public class FlightController : ControllerBase
{
    private readonly IFlightRepository _repository;
    private readonly IAircraftModelRepository _modelRepository;
    private readonly IMapper _mapper;

    public FlightController(IFlightRepository repository, IAircraftModelRepository modelRepository, IMapper mapper)
    {
        _repository = repository;
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightGetDto>>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<FlightGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightGetDto>> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(_mapper.Map<FlightGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(FlightEditDto dto)
    {
        var model = await _modelRepository.GetByIdAsync(dto.AircraftModelId);
        if (model == null) return BadRequest($"AircraftModel with Id {dto.AircraftModelId} not found");

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

        await _repository.AddAsync(flight);
        return CreatedAtAction(nameof(GetById), new { id = flight.Id }, _mapper.Map<FlightGetDto>(flight));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, FlightEditDto dto)
    {
        if (!await _repository.ExistsByIdAsync(id)) return NotFound();

        var model = await _modelRepository.GetByIdAsync(dto.AircraftModelId);
        if (model == null) return BadRequest($"AircraftModel with Id {dto.AircraftModelId} not found");

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

        await _repository.UpdateAsync(flight);
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
