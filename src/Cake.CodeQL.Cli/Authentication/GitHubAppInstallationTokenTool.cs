namespace Cake.GitHub.Authentication;

/// <summary>
/// Tool used to create an installation token for a GitHub App
/// </summary>
public class GitHubAppInstallationTokenTool
{
    private const string InstallationTokenEndpoint = "https://api.github.com/app/installations/{0}/access_tokens";

    private readonly ICakeContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubAppInstallationTokenTool"/> class.
    /// </summary>
    /// <param name="cakeContext">The cake Context</param>
    public GitHubAppInstallationTokenTool(ICakeContext cakeContext) => _context = cakeContext;

    /// <summary>
    /// Generates token
    /// </summary>
    /// <param name="settings"></param>
    /// <returns>Access token</returns>
    public AccessToken GenerateInstallationToken(GitHubAppInstallationTokenToolSettings settings)
    {
        using var rsa = RSA.Create();

        rsa.ImportFromPem(settings.PrivateKey.ToCharArray());

        var jwt = JwtBuilder.Create()
                         .WithAlgorithm(new RS256Algorithm(rsa, rsa))
                         .IssuedAt(DateTimeOffset.UtcNow.AddSeconds(-60).ToUnixTimeSeconds())
                         .ExpirationTime(DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
                         .Issuer(settings.AppId.ToString())
                         .Encode();

        var url = string.Format(InstallationTokenEndpoint, settings.InstallationId);

        var json = _context.HttpPost(url, settings =>
        {
            settings.UseBearerAuthorization(jwt);
            settings.SetAccept("application/vnd.github+json");
            settings.AppendHeader("X-GitHub-Api-Version", "2022-11-28");
            settings.SuppressLogResponseRequestBodyOutput();
            settings.EnsureSuccessStatusCode();
            settings.RequestBody = new byte[] { };
        });

        return Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(json);
    }
}
