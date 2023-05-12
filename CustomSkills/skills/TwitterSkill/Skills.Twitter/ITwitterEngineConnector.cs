namespace Skills.Twitter;

public interface ITwitterEngineConnector
{

    /// <summary>
    /// Execute a post on Twitter.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>First snippet returned from search.</returns>
    Task<bool> TweetPost(string post, CancellationToken cancellationToken = default);

}
