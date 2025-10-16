using AirlineApp.Application.Dtos.PassengerDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/passengers")]
public class PassengerController : ControllerBase
{
    private readonly IPassengerRepository _repository;
    private readonly IMapper _mapper;

    public PassengerController(IPassengerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PassengerGetDto>>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PassengerGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PassengerGetDto>> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(_mapper.Map<PassengerGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(PassengerEditDto dto)
    {
        var entity = _mapper.Map<Passenger>(dto);
        await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<PassengerGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, PassengerEditDto dto)
    {
        if (!await _repository.ExistsByIdAsync(id)) return NotFound();
        var entity = _mapper.Map<Passenger>(dto);
        entity.Id = id;
        await _repository.UpdateAsync(entity);
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
