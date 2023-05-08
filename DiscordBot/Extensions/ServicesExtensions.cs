using DiscordBot.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Extensions;

internal static class ServicesExtensions
{
    /// <summary>
    /// Parse configuration into options.
    /// </summary>
    internal static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // General  configuration
        services.AddOptions<ServiceOptions>()
            .Bind(configuration.GetSection(ServiceOptions.PropertyName))
            .ValidateDataAnnotations().ValidateOnStart();

        // AI service configurations
        services.AddOptions<AIServiceOptions>(AIServiceOptions.CompletionPropertyName)
            .Bind(configuration.GetSection(AIServiceOptions.CompletionPropertyName))
            .ValidateDataAnnotations().ValidateOnStart();

        // AI service configurations
        services.AddOptions<AIServiceOptions>(AIServiceOptions.TextCompletionPropertyName)
            .Bind(configuration.GetSection(AIServiceOptions.TextCompletionPropertyName))
            .ValidateDataAnnotations().ValidateOnStart();

        services.AddOptions<AIServiceOptions>(AIServiceOptions.EmbeddingPropertyName)
            .Bind(configuration.GetSection(AIServiceOptions.EmbeddingPropertyName))
            .ValidateDataAnnotations().ValidateOnStart();


        return services;
    }


}

