using AirlineApp.Application.Dtos.FlightDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/flights")]
public class FlightController(IFlightRepository repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightGetDto>>> GetAll()
    {
        var entities = await repository.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<FlightGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightGetDto>> GetById(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(mapper.Map<FlightGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(FlightEditDto dto)
    {
        var entity = mapper.Map<Flight>(dto);
        await repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, mapper.Map<FlightGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, FlightEditDto dto)
    {
        if (!await repository.ExistsByIdAsync(id)) return NotFound();
        var entity = mapper.Map<Flight>(dto);
        entity.Id = id;
        await repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await repository.ExistsByIdAsync(id)) return NotFound();
        await repository.DeleteAsync(id);
        return NoContent();
    }
}
