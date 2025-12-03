using AirlineApp.Generator.Generator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirlineApp.Generator.Services;

/// <summary>
/// Service for generating and sending a specified number of contracts at specified intervals
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="GeneratorService"/> class.
/// </remarks>
/// <param name="configuration">Application configuration</param>
/// <param name="producerService">Producer service for sending messages</param>
/// <param name="logger">Logger</param>
public class GeneratorService(
    IConfiguration configuration,
    IProducerService producerService,
    ILogger<GeneratorService> logger) : BackgroundService, IDisposable
{
    private readonly ILogger<GeneratorService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private bool _disposed;

    private IConfiguration Configuration { get; } = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private IProducerService ProducerService { get; } = producerService ?? throw new ArgumentNullException(nameof(producerService));

    /// <summary>
    /// Executes the background service to generate and send messages.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token to stop the service gracefully.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        try
        {
            var batchSize = GetConfigurationValue("Generator:BatchSize", "BatchSize");
            var payloadLimit = GetConfigurationValue("Generator:PayloadLimit", "PayloadLimit");
            var waitTime = GetConfigurationValue("Generator:WaitTime", "WaitTime");

            _logger.LogInformation(
                "Starting to send {Total} messages with {Time}s interval with {Batch} messages in batch",
                payloadLimit, waitTime, batchSize);

            var counter = 0;
            while (counter < payloadLimit && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProducerService.SendAsync(TicketGenerator.GenerateTickets(batchSize));
                    counter += batchSize;

                    _logger.LogDebug("Sent {Counter} of {Total} messages", counter, payloadLimit);

                    if (counter < payloadLimit)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(waitTime), stoppingToken);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Generator service was cancelled");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while sending batch");
                    await Task.Delay(TimeSpan.FromSeconds(Math.Min(waitTime * 2, 60)), stoppingToken);
                }
            }

            _logger.LogInformation(
                "Finished sending {Total} messages with {Time}s interval with {Batch} messages in batch",
                payloadLimit, waitTime, batchSize);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogCritical(ex, "Generator service failed to start");
            throw;
        }
    }

    /// <summary>
    /// Gets and parses configuration value.
    /// </summary>
    /// <param name="sectionPath">Path to configuration section</param>
    /// <param name="parameterName">Parameter name for error messages</param>
    /// <returns>Parsed integer value</returns>
    /// <exception cref="KeyNotFoundException">If configuration value is missing</exception>
    /// <exception cref="FormatException">If configuration value cannot be parsed</exception>
    private int GetConfigurationValue(string sectionPath, string parameterName)
    {
        var valueStr = Configuration[sectionPath]
            ?? throw new KeyNotFoundException($"{parameterName} section is missing");

        if (!int.TryParse(valueStr, out var value))
            throw new FormatException($"Unable to parse {parameterName}");

        if (value <= 0)
            throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} must be greater than 0");

        return value;
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="GeneratorService"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (ProducerService is IDisposable disposableProducer)
                {
                    disposableProducer.Dispose();
                    _logger.LogDebug("ProducerService disposed");
                }
            }

            _disposed = true;
        }
    }

    /// <summary>
    /// Releases all resources used by the <see cref="GeneratorService"/>.
    /// </summary>
    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        base.Dispose();
    }
}