using AirlineApp.Application.Dtos.AircraftModelDtos;
using AirlineApp.Domain.Entities;
using AirlineApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/aircraft-models")]
public class AircraftModelController : ControllerBase
{
    private readonly IAircraftModelRepository _repository;
    private readonly IAircraftFamilyRepository _familyRepository;
    private readonly IMapper _mapper;

    public AircraftModelController(IAircraftModelRepository repository, IAircraftFamilyRepository familyRepository, IMapper mapper)
    {
        _repository = repository;
        _familyRepository = familyRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftModelGetDto>>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<AircraftModelGetDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftModelGetDto>> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(_mapper.Map<AircraftModelGetDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult> Create(AircraftModelEditDto dto)
    {
        var family = await _familyRepository.GetByIdAsync(dto.FamilyId);
        if (family == null) return BadRequest($"AircraftFamily with Id {dto.FamilyId} not found");

        var entity = new AircraftModel
        {
            Name = dto.Name,
            FlightRange = dto.FlightRange,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            Family = family
        };

        await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<AircraftModelGetDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, AircraftModelEditDto dto)
    {
        if (!await _repository.ExistsByIdAsync(id)) return NotFound();

        var family = await _familyRepository.GetByIdAsync(dto.FamilyId);
        if (family == null) return BadRequest($"AircraftFamily with Id {dto.FamilyId} not found");

        var entity = new AircraftModel
        {
            Id = id,
            Name = dto.Name,
            FlightRange = dto.FlightRange,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            Family = family
        };

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
