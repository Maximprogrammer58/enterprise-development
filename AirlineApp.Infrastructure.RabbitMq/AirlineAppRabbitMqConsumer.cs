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
public class AirlineAppRabbitMqConsumer : BackgroundService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AirlineAppRabbitMqConsumer> _logger;
    private readonly string _queueName;
    private readonly IModel _channel; 
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="AirlineAppRabbitMqConsumer"/> class.
    /// </summary>
    /// <param name="connection">RabbitMQ connection</param>
    /// <param name="scopeFactory">Service scope factory for creating scopes per message</param>
    /// <param name="configuration">Application configuration</param>
    /// <param name="logger">Logger</param>
    public AirlineAppRabbitMqConsumer(
        IConnection connection,
        IServiceScopeFactory scopeFactory,
        IConfiguration configuration,
        ILogger<AirlineAppRabbitMqConsumer> logger)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _queueName = configuration.GetSection("RabbitMq")["QueueName"]
            ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _logger.LogInformation("RabbitMQ Consumer initialized for queue: {QueueName}", _queueName);
    }

    /// <summary>
    /// Executes the background service to start consuming messages from RabbitMQ queue.
    /// Establishes connection, creates channel, and begins listening for incoming messages.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token to stop the service gracefully.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting to listen to queue {Queue}", _queueName);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) => await ReceiveMessage(ea, stoppingToken);

        _channel.BasicConsume(_queueName, autoAck: true, consumer: consumer);

        _logger.LogInformation("Successfully started listening to queue {Queue}", _queueName);

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
        try
        {
            _logger.LogDebug("Received a message from queue {Queue}", _queueName);

            stoppingToken.ThrowIfCancellationRequested();

            var contracts = JsonSerializer.Deserialize<List<TicketEditDto>>(args.Body.Span)
                ?? throw new FormatException("Unable to parse contracts from message body");

            using var scope = _scopeFactory.CreateScope();
            var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();

            await ticketService.ReceiveContractList(contracts);

            _logger.LogDebug("Successfully processed batch of {Count} contracts", contracts.Count);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Message processing was cancelled");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during processing contracts from {Queue}", _queueName);
        }
    }

    /// <summary>
    /// Disposes the resources used by the consumer.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _channel?.Close();
                _channel?.Dispose();
            }
            _disposed = true;
        }
    }

    /// <summary>
    /// Disposes the consumer.
    /// </summary>
    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}