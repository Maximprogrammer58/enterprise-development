using AirlineApp.Contracts.Dtos.AircraftModelDtos;
using AirlineApp.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

/// <summary>
/// Controller for managing Aircraft Models.
/// Provides endpoints to create, read, update, and delete aircraft model data.
/// </summary>
[ApiController]
[Route("api/aircraft-models")]
public class AircraftModelController(ICrudService<AircraftModelGetDto, AircraftModelEditDto> service) : ControllerBase
{
    /// <summary>
    /// Retrieves all aircraft models.
    /// </summary>
    /// <returns>A list of <see cref="AircraftModelGetDto"/> objects.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<AircraftModelGetDto>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Retrieves a specific aircraft model by its Id.
    /// </summary>
    /// <param name="id">The Id of the aircraft model to retrieve.</param>
    /// <returns>An <see cref="AircraftModelGetDto"/> if found; otherwise, 404 NotFound.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<AircraftModelGetDto>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a new aircraft model.
    /// </summary>
    /// <param name="dto">The data for the new aircraft model.</param>
    /// <returns>The created aircraft model with its Id.
    /// 400 Bad Request — if the provided data is invalid.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<AircraftModelGetDto>> Create([FromBody] AircraftModelEditDto dto)
    {
        var result = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing aircraft model.
    /// </summary>
    /// <param name="id">The Id of the aircraft model to update.</param>
    /// <param name="dto">The updated aircraft model data.</param>
    /// <returns>NoContent if updated; 400 BadRequest if invalid; 404 NotFound if not found.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] AircraftModelEditDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes an aircraft model by its Id.
    /// </summary>
    /// <param name="id">The Id of the aircraft model to delete.</param>
    /// <returns>NoContent if deleted; 404 NotFound if not found; 
    /// 400 BadRequest if deletion is not allowed due to existing linked flights.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
