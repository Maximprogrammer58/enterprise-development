using AirlineApp.Application.Services;
using AirlineApp.Contracts.Dtos.AircraftFamilyDtos;
using AirlineApp.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

/// <summary>
/// Controller for managing Aircraft Families.
/// Provides endpoints to create, read, update, and delete aircraft family data.
/// </summary>
[ApiController]
[Route("api/aircraft-families")]
public class AircraftFamilyController(ICrudService<AircraftFamilyGetDto, AircraftFamilyEditDto> service) : ControllerBase
{
    /// <summary>
    /// Retrieves all aircraft families.
    /// </summary>
    /// <returns>A list of <see cref="AircraftFamilyGetDto"/> objects.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftFamilyGetDto>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Retrieves a specific aircraft family by its Id.
    /// </summary>
    /// <param name="id">The Id of the aircraft family to retrieve.</param>
    /// <returns>An <see cref="AircraftFamilyGetDto"/> if found; otherwise, 404 NotFound.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftFamilyGetDto>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a new aircraft family.
    /// </summary>
    /// <param name="dto">The data for the new aircraft family.</param>
    /// <returns>The created aircraft family with its Id.</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AircraftFamilyEditDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing aircraft family.
    /// </summary>
    /// <param name="id">The Id of the aircraft family to update.</param>
    /// <param name="dto">The updated aircraft family data.</param>
    /// <returns>NoContent if updated; 404 NotFound if not found.</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AircraftFamilyEditDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes an aircraft family by its Id.
    /// </summary>
    /// <param name="id">The Id of the aircraft family to delete.</param>
    /// <returns>NoContent if deleted; 404 NotFound if not found; 
    /// 400 BadRequest if deletion is not allowed due to existing linked data.</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
