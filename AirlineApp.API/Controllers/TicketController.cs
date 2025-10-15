using AirlineApp.Application.Dtos.TicketDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController(ITicketRepository repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketGetDto>>> GetAll()
    {
        var entities = await repository.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<TicketGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketGetDto>> GetById(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(mapper.Map<TicketGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(TicketEditDto dto)
    {
        var entity = mapper.Map<Ticket>(dto);
        await repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, mapper.Map<TicketGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, TicketEditDto dto)
    {
        if (!await repository.ExistsByIdAsync(id)) return NotFound();
        var entity = mapper.Map<Ticket>(dto);
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