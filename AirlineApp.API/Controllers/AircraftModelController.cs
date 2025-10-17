using AirlineApp.Application.Dtos.AircraftModelDtos;
using AirlineApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/aircraft-models")]
public class AircraftModelController : ControllerBase
{
    private readonly AircraftModelService _service;

    public AircraftModelController(AircraftModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftModelGetDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftModelGetDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(AircraftModelEditDto dto)
    {
        var (success, result, error) = await _service.CreateAsync(dto);
        if (!success) return BadRequest(error);
        return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, AircraftModelEditDto dto)
    {
        var (success, error) = await _service.UpdateAsync(id, dto);
        if (!success) return BadRequest(error ?? "Not found");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, error) = await _service.DeleteAsync(id);
        if (!success)
            return string.IsNullOrEmpty(error) ? NotFound() : BadRequest(error);
        return NoContent();
    }
}
