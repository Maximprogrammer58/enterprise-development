using AirlineApp.Application.Dtos.AircraftFamilyDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/aircraft-families")]
public class AircraftFamilyController : ControllerBase
{
    private readonly IAircraftFamilyRepository _repository;
    private readonly IMapper _mapper;

    public AircraftFamilyController(IAircraftFamilyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftFamilyGetDto>>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<AircraftFamilyGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftFamilyGetDto>> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(_mapper.Map<AircraftFamilyGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(AircraftFamilyEditDto dto)
    {
        var entity = _mapper.Map<AircraftFamily>(dto);
        await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<AircraftFamilyGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, AircraftFamilyEditDto dto)
    {
        if (!await _repository.ExistsByIdAsync(id)) return NotFound();
        var entity = _mapper.Map<AircraftFamily>(dto);
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
