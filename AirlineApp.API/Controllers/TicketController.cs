using AirlineApp.Application.Dtos.TicketDtos;
using AirlineApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.API.Controllers;

/// <summary>
/// Controller for managing Tickets.
/// Provides endpoints to create, read, update, and delete ticket records.
/// </summary>
[ApiController]
[Route("api/tickets")]
public class TicketController(TicketService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all tickets.
    /// </summary>
    /// <returns>A list of <see cref="TicketGetDto"/> objects.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketGetDto>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Retrieves a specific ticket by Id.
    /// </summary>
    /// <param name="id">The Id of the ticket to retrieve.</param>
    /// <returns>A <see cref="TicketGetDto"/> if found; otherwise, 404 NotFound.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketGetDto>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a new ticket.
    /// </summary>
    /// <param name="dto">The data for the new ticket.</param>
    /// <returns>The created ticket with its Id.</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TicketEditDto dto)
    {
        var result = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing ticket.
    /// </summary>
    /// <param name="id">The Id of the ticket to update.</param>
    /// <param name="dto">The updated ticket data.</param>
    /// <returns>NoContent if updated; 404 NotFound or 400 BadRequest if invalid.</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] TicketEditDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a ticket by Id.
    /// </summary>
    /// <param name="id">The Id of the ticket to delete.</param>
    /// <returns>NoContent if deleted; 404 NotFound if ticket not found.</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
