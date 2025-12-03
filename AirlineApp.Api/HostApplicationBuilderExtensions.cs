using AirlineApp.Infrastructure.RabbitMq;

namespace AirlineApp.Api;

/// <summary>
/// Extension class for registering RabbitMQ consumer in the DI container
/// </summary>
internal static class HostApplicationBuilderExtensions
{
    /// <summary>
    /// Registers RabbitMQ consumer service
    /// </summary>
    /// <param name="builder">Host application builder</param>
    /// <param name="configuration">Configuration</param>
    /// <returns>Host application builder with registered services</returns>
    /// <exception cref="ArgumentNullException">If the RabbitMQ configuration is not found</exception>
    public static IHostApplicationBuilder AddRabbitMqConsumer(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        if (!configuration.GetSection("RabbitMq").Exists())
            throw new ArgumentNullException(nameof(configuration), "RabbitMq section is missing");

        return AddRabbitMqServices(builder);
    }

    /// <summary>
    /// Registers the message broker client and the services required for its operation
    /// </summary>
    /// <param name="builder">Host application builder</param>
    /// <returns>Host application builder with registered RabbitMq services</returns>
    private static IHostApplicationBuilder AddRabbitMqServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<AirlineAppRabbitMqConsumer>();

        builder.Services.AddSingleton<IHostedService>(provider =>
            provider.GetRequiredService<AirlineAppRabbitMqConsumer>());

        builder.AddRabbitMQClient("airlineapp-rabbitmq");

        return builder;
    }
}