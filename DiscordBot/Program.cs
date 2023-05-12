
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBot;
using DiscordBot.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skills.Twitter.Config;

using IHost host = Host.CreateDefaultBuilder(args)

    .ConfigureAppConfiguration(config =>
    {
        //config.AddJsonFile("prompts.json", optional: false, reloadOnChange: true);
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices((hostContext, services) =>
    {

        services.AddOptions(hostContext.Configuration);
        services.Configure<TwitterConfig>(hostContext.Configuration.GetSection("Twitter"));
        services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(sp => sp.GetRequiredService<ILogger<Program>>());



        services.AddSingleton<DiscordSocketClient>();       // Add the discord client to services
        services.AddSingleton<InteractionService>();        // Add the interaction service to services
        services.AddHostedService<InteractionHandlingService>();    // Add the slash command handler
        services.AddHostedService<DiscordStartupService>();         // Add the discord startup service
    })
    .Build();


await host.RunAsync();