using AirlineApp.Contracts.Dtos.TicketDtos;

namespace AirlineApp.Contracts.Interfaces;
public interface ITicketService : ICrudService<TicketGetDto, TicketEditDto>
{
    public Task ReceiveContractList(IList<TicketEditDto> contracts);
}