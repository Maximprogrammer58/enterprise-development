using AirlineApp.Contracts.Dtos.FlightDtos;
using AirlineApp.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

/// <summary>
/// Controller for managing Flights.
/// Provides endpoints to create, read, update, and delete flight data.
/// </summary>
[ApiController]
[Route("api/flights")]
public class FlightController(ICrudService<FlightGetDto, FlightEditDto> service) : ControllerBase
{
    /// <summary>
    /// Retrieves all flights.
    /// </summary>
    /// <returns>A list of <see cref="FlightGetDto"/> objects.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<FlightGetDto>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Retrieves a specific flight by its Id.
    /// </summary>
    /// <param name="id">The Id of the flight to retrieve.</param>
    /// <returns>A <see cref="FlightGetDto"/> if found; otherwise, 404 NotFound.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
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
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Create([FromBody] FlightEditDto dto)
    {
        var result = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing flight.
    /// </summary>
    /// <param name="id">The Id of the flight to update.</param>
    /// <param name="dto">The updated flight data.</param>
    /// <returns>NoContent if updated; 400 BadRequest if invalid; 404 NotFound if flight not found.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] FlightEditDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a flight by its Id.
    /// </summary>
    /// <param name="id">The Id of the flight to delete.</param>
    /// <returns>NoContent if deleted; 404 NotFound if flight not found.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
