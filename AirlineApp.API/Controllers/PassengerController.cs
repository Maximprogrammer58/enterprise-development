using AirlineApp.Application.Dtos.PassengerDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/passengers")]
public class PassengerController(IPassengerRepository repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PassengerGetDto>>> GetAll()
    {
        var entities = await repository.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<PassengerGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PassengerGetDto>> GetById(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(mapper.Map<PassengerGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(PassengerEditDto dto)
    {
        var entity = mapper.Map<Passenger>(dto);
        await repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, mapper.Map<PassengerGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, PassengerEditDto dto)
    {
        if (!await repository.ExistsByIdAsync(id)) return NotFound();
        var entity = mapper.Map<Passenger>(dto);
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
