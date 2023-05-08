using Discord.Interactions;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Modules
{
    public class PingModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static ILogger<PingModule> _logger;

        public PingModule(ILogger<PingModule> logger)
        {
            _logger = logger;
        }


        // Basic slash command. [SlashCommand("name", "description")]
        // Similar to text command creation, and their respective attributes
        [SlashCommand("ping", "Receive a pong!")]
        public async Task Ping()
        {
            // New LogMessage created to pass desired info to the console using the existing Discord.Net LogMessage parameters
            _logger.LogInformation($"PingModule : Ping User: {Context.User.Username}, Command: ping");
            // Respond to the user
            await RespondAsync("pong");
        }
    }
}
