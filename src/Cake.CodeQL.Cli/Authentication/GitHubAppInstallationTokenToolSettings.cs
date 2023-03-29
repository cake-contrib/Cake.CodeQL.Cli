namespace Cake.GitHub.Authentication;

/// <summary>
/// Settings needed to generate a GItHub App Installation Token. Used to get an installation token
/// </summary>
/// <remarks>See links below for more information.
/// <list type="bullet">
///  <item><seealso href="https://docs.github.com/en/apps/creating-github-apps/authenticating-with-a-github-app/generating-a-json-web-token-jwt-for-a-github-app">Generating a JSON Web Token (JWT) for a GitHub App</seealso></item>
///  <item><seealso href="https://docs.github.com/en/apps/creating-github-apps/authenticating-with-a-github-app/generating-an-installation-access-token-for-a-github-app">Generating an installation access token for a GitHub App</seealso></item>
/// </list>
/// </remarks>
public class GitHubAppInstallationTokenToolSettings
{
    /// <summary>
    /// unique application is for a GitHub App
    /// </summary>
    public int AppId { get; set; }

    /// <summary>
    /// Installation Id associated with the app.
    /// </summary>
    public int InstallationId { get; set; }

    /// <summary>
    /// Private Key generated in github org.  The private key is the SINGLE most valuable secret for a GitHub App. We recommend storing the key in a key vault, such as Azure Key Vault or HashiCorp
    /// </summary>
    /// <remarks><seealso href="https://docs.github.com/en/apps/creating-github-apps/authenticating-with-a-github-app/managing-private-keys-for-github-apps">Managing private keys for GitHub Apps</seealso></remarks>
    public string PrivateKey { get; set; }
}