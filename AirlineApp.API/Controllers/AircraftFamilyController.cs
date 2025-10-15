using AirlineApp.Application.Dtos.AircraftFamilyDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/aircraft-families")]
public class AircraftFamilyController(IAircraftFamilyRepository repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftFamilyGetDto>>> GetAll()
    {
        var entities = await repository.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<AircraftFamilyGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftFamilyGetDto>> GetById(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(mapper.Map<AircraftFamilyGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(AircraftFamilyEditDto dto)
    {
        var entity = mapper.Map<AircraftFamily>(dto);
        await repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, mapper.Map<AircraftFamilyGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, AircraftFamilyEditDto dto)
    {
        if (!await repository.ExistsByIdAsync(id)) return NotFound();
        var entity = mapper.Map<AircraftFamily>(dto);
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