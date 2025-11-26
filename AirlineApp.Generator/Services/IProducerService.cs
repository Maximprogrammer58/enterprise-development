using AirlineApp.Contracts.Dtos.TicketDtos;

namespace AirlineApp.Generator.Services;

/// <summary>
/// Interface of the service that sends messages over the bus
/// </summary>
public interface IProducerService
{
    /// <summary>
    /// Method for sending a collection of contracts
    /// </summary>
    /// <param name="batch">Collection of contracts</param>
    public Task SendAsync(IList<TicketEditDto> batch);
}