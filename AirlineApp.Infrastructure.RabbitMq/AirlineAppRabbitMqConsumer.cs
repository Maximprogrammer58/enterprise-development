using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace AirlineApp.Infrastructure.RabbitMq;

/// <summary>
/// Background service for consuming ticket messages from RabbitMQ queue.
/// Listens to the specified queue, processes incoming messages, and saves ticket data to the database.
/// </summary>
public class AirlineAppRabbitMqConsumer(
    IConnection connection,
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    ILogger<AirlineAppRabbitMqConsumer> logger) : BackgroundService
{
    private readonly IConnection _connection = connection;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ILogger<AirlineAppRabbitMqConsumer> _logger = logger;
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"]
            ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

    /// <summary>
    /// Executes the background service to start consuming messages from RabbitMQ queue.
    /// Establishes connection, creates channel, and begins listening for incoming messages.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token to stop the service gracefully.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Establishing channel to queue {queue}", _queueName);

        stoppingToken.ThrowIfCancellationRequested();
        var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        _logger.LogInformation("Began listening to queue {queue}", _queueName);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (_, ea) => await ReceiveMessage(ea, stoppingToken);
        channel.BasicConsume(_queueName, true, consumer);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles incoming RabbitMQ messages by deserializing ticket data and processing through ticket service.
    /// </summary>
    /// <param name="args">The event arguments containing the message data.</param>
    /// <param name="stoppingToken">Cancellation token for graceful operation termination.</param>
    /// <returns>A task that represents the asynchronous message processing operation.</returns>
    private async Task ReceiveMessage(BasicDeliverEventArgs args, CancellationToken stoppingToken)
    {
        _logger.LogInformation("Received a message from queue {queue}", _queueName);
        try
        {
            stoppingToken.ThrowIfCancellationRequested();
            var contracts = JsonSerializer.Deserialize<List<TicketEditDto>>(new MemoryStream(args.Body.ToArray()))
                ?? throw new FormatException("Unable to parse contracts from message body");

            using var scope = _scopeFactory.CreateScope();
            var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();
            await ticketService.ReceiveContractList(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during receiving contracts from {queue}", _queueName);
        }
    }
}