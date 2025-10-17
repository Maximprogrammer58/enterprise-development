using AirlineApp.Application.Dtos.AircraftFamilyDtos;
using AirlineApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/aircraft-families")]
public class AircraftFamilyController : ControllerBase
{
    private readonly AircraftFamilyService _service;

    public AircraftFamilyController(AircraftFamilyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftFamilyGetDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftFamilyGetDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(AircraftFamilyEditDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, AircraftFamilyEditDto dto)
    {
        var success = await _service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, errorMessage) = await _service.DeleteAsync(id);

        if (!success)
        {
            return string.IsNullOrEmpty(errorMessage) ? NotFound() : BadRequest(errorMessage);
        }

        return NoContent();
    }
}
