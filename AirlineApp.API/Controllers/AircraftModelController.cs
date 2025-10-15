using AirlineApp.Application.Dtos.AircraftModelDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/aircraft-models")]
public class AircraftModelController(IAircraftModelRepository repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftModelGetDto>>> GetAll()
    {
        var entities = await repository.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<AircraftModelGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftModelGetDto>> GetById(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(mapper.Map<AircraftModelGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(AircraftModelEditDto dto)
    {
        var entity = mapper.Map<AircraftModel>(dto);
        await repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, mapper.Map<AircraftModelGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, AircraftModelEditDto dto)
    {
        if (!await repository.ExistsByIdAsync(id)) return NotFound();
        var entity = mapper.Map<AircraftModel>(dto);
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