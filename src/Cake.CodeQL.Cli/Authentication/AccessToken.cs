namespace Cake.GitHub.Authentication;

public class AccessToken
{
    /// <summary>
    /// The access token
    /// </summary>
    [Newtonsoft.Json.JsonProperty("token")]
    public string Token { get; init; }

    /// <summary>
    /// The expiration date
    /// </summary>
    [Newtonsoft.Json.JsonProperty("expires_at")]
    public DateTimeOffset ExpiresAt { get; init; }
}