using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
using Skills.Twitter.Config;
using Tweetinvi;

namespace Skills.Twitter;

public class TwitterEngineSkill
{

    private TwitterClient _twitterClient { get; set; }
    private readonly ILogger _logger;
    private readonly TwitterConfig _twitterConfig;
    public TwitterEngineSkill(TwitterConfig twitterConfig, ILogger<TwitterEngineSkill> logger = null)
    {
        this._logger = logger ?? NullLogger<TwitterEngineSkill>.Instance;
        _twitterConfig = twitterConfig;
        _twitterClient = new TwitterClient(_twitterConfig.ConsumerKey, _twitterConfig.ConsumerSecret, _twitterConfig.AccessSecret, _twitterConfig.AccessSecret);
    }



    [SKFunction("upload a new tweet on twitter")]
    [SKFunctionName("UploadPostOnTwitter")]
    [SKFunctionInput(Description = "Tweet to post on twitter")]
    public async Task<string> UploadPostOnTwitterAsync(string tweet, SKContext context)
    {
        try
        {
            var user = await _twitterClient.Users.GetAuthenticatedUserAsync();
            await _twitterClient.Tweets.PublishTweetAsync(tweet);
            return tweet;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errors post tweet");
            throw;
        }
    }
}