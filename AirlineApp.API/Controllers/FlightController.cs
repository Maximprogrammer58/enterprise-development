using AirlineApp.Application.Dtos.FlightDtos;
using AirlineApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

[ApiController]
[Route("api/flights")]
public class FlightController : ControllerBase
{
    private readonly FlightService _service;

    public FlightController(FlightService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightGetDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightGetDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(FlightEditDto dto)
    {
        var (success, result, error) = await _service.CreateAsync(dto);
        if (!success) return BadRequest(error);
        return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, FlightEditDto dto)
    {
        var (success, error) = await _service.UpdateAsync(id, dto);
        if (!success) return BadRequest(error ?? "Not found");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
