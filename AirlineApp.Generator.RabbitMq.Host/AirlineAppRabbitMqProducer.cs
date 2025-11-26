using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Generator.Services;
using RabbitMQ.Client;
using System.Text.Json;

namespace AirlineApp.Generator.RabbitMq.Host;

/// <summary>
/// Implementation for sending contracts via a RabbitMq queue
/// </summary>
/// <param name="configuration">Configuration</param>
/// <param name="rabbitMqConnection">Connection to the message broker</param>
/// <param name="logger">Logger</param>
public class TicketRabbitMqProducer(IConfiguration configuration, IConnection rabbitMqConnection, ILogger<TicketRabbitMqProducer> logger) : IProducerService
{
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"] ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

    public Task SendAsync(IList<TicketEditDto> batch)
    {
        try
        {
            logger.LogInformation("Sending a batch of {count} contracts to {queue}", batch.Count, _queueName);
            var payload = JsonSerializer.SerializeToUtf8Bytes(batch);

            using var channel = rabbitMqConnection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false);
            channel.BasicPublish(exchange: string.Empty, routingKey: _queueName, mandatory: false, body: payload);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during sending a batch of {count} contracts to {queue}", batch.Count, _queueName);
            return Task.CompletedTask;
        }
    }
}