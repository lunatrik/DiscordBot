using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Skills.Twitter.Config;
using System.Diagnostics;
using Tweetinvi;
using Tweetinvi.Models;

namespace DiscordBot.Modules;

public class TwitterModule : InteractionModuleBase<SocketInteractionContext>
{

    private static ILogger _logger;

    private static TwitterConfig _twitterConfig;
    private static IAuthenticationRequest authenticationRequest;
    public TwitterModule(ILogger<PlanModule> logger, IOptions<TwitterConfig> twitterConfig)
    {
        _logger = logger;

        _twitterConfig = twitterConfig.Value;

    }

    [SlashCommand("twitter-auth", "Auth Twitter")]
    public async Task TwitterAuth()
    {
        await DeferAsync();

        var appClient = new TwitterClient(_twitterConfig.ConsumerKey, _twitterConfig.ConsumerSecret);

        // Start the authentication process
        authenticationRequest = await appClient.Auth.RequestAuthenticationUrlAsync();

        // Go to the URL so that Twitter authenticates the user and gives him a PIN code.

        var modalBuilder = new ModalBuilder("Twitter Auth", "auth-code-pin")
            .AddTextInput(new TextInputBuilder("Please enter the code and press enter.", "ping-code"))
            ;

        Process.Start(new ProcessStartInfo(authenticationRequest.AuthorizationURL)
        {
            UseShellExecute = true
        });

        await Context.Interaction.FollowupAsync("Please enter the code and press enter. /twitter-pin", ephemeral: true);
    }

    [SlashCommand("twitter-pin", "insert twitter pin auth")]
    public async Task TwitterPin(string pinCode)
    {
        await DeferAsync();
        var appClient = new TwitterClient(_twitterConfig.ConsumerKey, _twitterConfig.ConsumerSecret);
        // With this pin code it is now possible to get the credentials back from Twitter
        var userCredentials = await appClient.Auth.RequestCredentialsFromVerifierCodeAsync(pinCode, authenticationRequest);

        // You can now save those credentials or use them as followed
        var userClient = new TwitterClient(userCredentials);
        var user = await userClient.Users.GetAuthenticatedUserAsync();

        var userEmbed = new EmbedBuilder()
            .WithTitle("Twitter Authentication Successful")
            .WithDescription($"Congratulations! You have authenticated the user: **{user}**.")
            .WithColor(Color.Green)
            .Build();

        await ReplyAsync(embed: userEmbed);

    }

}
