using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Generator.Services;
using RabbitMQ.Client;
using System.Text.Json;

namespace AirlineApp.Generator.RabbitMq.Host;

/// <summary>
/// Implementation for sending contracts via a RabbitMq queue
/// </summary>
public class TicketRabbitMqProducer : IProducerService, IDisposable
{
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly ILogger<TicketRabbitMqProducer> _logger;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="TicketRabbitMqProducer"/> class.
    /// </summary>
    /// <param name="configuration">Application configuration</param>
    /// <param name="rabbitMqConnection">RabbitMQ connection</param>
    /// <param name="logger">Logger</param>
    /// <exception cref="ArgumentNullException">If any parameter is null</exception>
    /// <exception cref="KeyNotFoundException">If QueueName is not found in configuration</exception>
    public TicketRabbitMqProducer(
        IConfiguration configuration,
        IConnection rabbitMqConnection,
        ILogger<TicketRabbitMqProducer> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        ArgumentNullException.ThrowIfNull(configuration);

        ArgumentNullException.ThrowIfNull(rabbitMqConnection);

        _queueName = configuration.GetSection("RabbitMq")["QueueName"]
            ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

        if (!rabbitMqConnection.IsOpen)
        {
            _logger.LogWarning("RabbitMQ connection is not open, attempting to create channel anyway");
        }

        _channel = rabbitMqConnection.CreateModel();
        _channel.QueueDeclare(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false);

        _logger.LogInformation("Producer created for queue: {QueueName}", _queueName);
    }

    public Task SendAsync(IList<TicketEditDto> batch)
    {
        try
        {
            _logger.LogInformation("Sending a batch of {Count} contracts to {Queue}", batch.Count, _queueName);
            var payload = JsonSerializer.SerializeToUtf8Bytes(batch);

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueName,
                mandatory: false,
                body: payload);

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during sending a batch of {Count} contracts to {Queue}",
                batch.Count, _queueName);
            throw; 
        }
    }

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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}