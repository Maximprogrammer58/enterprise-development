using Bogus;
using AirlineApp.Contracts.Dtos.TicketDtos;

namespace AirlineApp.Generator.Generator;

/// <summary>
/// Static class for generating TicketEditDto contracts
/// </summary>
public static class TicketGenerator
{
    /// <summary>
    /// Method for generating a specified number of TicketEditDto contracts
    /// </summary>
    /// <param name="count">Number of contracts to generate</param>
    /// <returns>TicketEditDto collection</returns>
    public static List<TicketEditDto> GenerateTickets(int count) =>
        new Faker<TicketEditDto>()
            .RuleFor(t => t.FlightId, f => f.Random.Int(1, 10)) 
            .RuleFor(t => t.PassengerId, f => f.Random.Int(1, 10)) 
            .RuleFor(t => t.SeatNumber, f => $"{f.Random.Char('A', 'F')}{f.Random.Int(1, 30)}") 
            .RuleFor(t => t.HasHandLuggage, f => f.Random.Bool())
            .RuleFor(t => t.BaggageWeight, f => f.Random.Bool() ? f.Random.Double(1, 30) : null) 
            .Generate(count);
}
