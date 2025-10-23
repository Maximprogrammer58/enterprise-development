using AirlineApp.Application.Services;
using AirlineApp.Contracts.Dtos.PassengerDtos;
using AirlineApp.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

/// <summary>
/// Controller for managing Passengers.
/// Provides endpoints to create, read, update, and delete passenger records.
/// </summary>
[ApiController]
[Route("api/passengers")]
public class PassengerController(ICrudService<PassengerGetDto, PassengerEditDto> service) : ControllerBase
{
    /// <summary>
    /// Retrieves all passengers.
    /// </summary>
    /// <returns>A list of <see cref="PassengerGetDto"/> objects.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PassengerGetDto>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Retrieves a specific passenger by Id.
    /// </summary>
    /// <param name="id">The Id of the passenger to retrieve.</param>
    /// <returns>A <see cref="PassengerGetDto"/> if found; otherwise, 404 NotFound.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PassengerGetDto>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a new passenger.
    /// </summary>
    /// <param name="dto">The data for the new passenger.</param>
    /// <returns>The created passenger with its Id.</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PassengerEditDto dto)
    {
        var result = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing passenger.
    /// </summary>
    /// <param name="id">The Id of the passenger to update.</param>
    /// <param name="dto">The updated passenger data.</param>
    /// <returns>NoContent if updated; 404 NotFound if passenger not found.</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] PassengerEditDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a passenger by Id.
    /// </summary>
    /// <param name="id">The Id of the passenger to delete.</param>
    /// <returns>NoContent if deleted; 404 NotFound if passenger not found.</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
