using AirlineApp.Application.Dtos.FlightDtos;
using AirlineApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

/// <summary>
/// Controller for managing Flights.
/// Provides endpoints to create, read, update, and delete flight data.
/// </summary>
[ApiController]
[Route("api/flights")]
public class FlightController(FlightService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all flights.
    /// </summary>
    /// <returns>A list of <see cref="FlightGetDto"/> objects.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightGetDto>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Retrieves a specific flight by its Id.
    /// </summary>
    /// <param name="id">The Id of the flight to retrieve.</param>
    /// <returns>A <see cref="FlightGetDto"/> if found; otherwise, 404 NotFound.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightGetDto>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a new flight.
    /// </summary>
    /// <param name="dto">The data for the new flight.</param>
    /// <returns>The created flight with its Id.</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] FlightEditDto dto)
    {
        var (success, result, error) = await service.CreateAsync(dto);
        if (!success) return BadRequest(error);
        return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
    }

    /// <summary>
    /// Updates an existing flight.
    /// </summary>
    /// <param name="id">The Id of the flight to update.</param>
    /// <param name="dto">The updated flight data.</param>
    /// <returns>NoContent if updated; 400 BadRequest if invalid; 404 NotFound if flight not found.</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] FlightEditDto dto)
    {
        var (success, error) = await service.UpdateAsync(id, dto);
        if (!success) return BadRequest(error ?? "Not found");
        return NoContent();
    }

    /// <summary>
    /// Deletes a flight by its Id.
    /// </summary>
    /// <param name="id">The Id of the flight to delete.</param>
    /// <returns>NoContent if deleted; 404 NotFound if flight not found.</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
