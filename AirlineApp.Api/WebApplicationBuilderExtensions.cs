using AirlineApp.Infrastructure.RabbitMq;

namespace AirlineApp.Api;

/// <summary>
/// Extension class for registering a suitable client for the generation service in the DI container
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Registers a client for interaction with the data generation service
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <param name="configuration">Configuration</param>
    /// <returns>Web application builder with registered services</returns>
    /// <exception cref="ArgumentNullException">If the Generator configuration parameter is not found</exception>
    /// <exception cref="FormatException">If the Generator configuration parameter is unknown</exception>
    public static WebApplicationBuilder AddGeneratorService(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        if (!configuration.GetSection("Generator").Exists())
            throw new ArgumentNullException(nameof(configuration), "Generator section is missing"); 

        _ = configuration["Generator"] switch
        {
            "RabbitMq" => AddRabbitMq(builder),
            _ => throw new FormatException("Unknown parameter in Generator section")
        };
        return builder;
    }

    /// <summary>
    /// Registers the message broker client and the services required for its operation
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder with registered RabbitMq services</returns>
    private static WebApplicationBuilder AddRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<AirlineAppRabbitMqConsumer>();
        builder.AddRabbitMQClient("airlineapp-rabbitmq");
        return builder;
    }
}