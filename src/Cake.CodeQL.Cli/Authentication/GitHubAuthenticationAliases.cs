namespace Cake.GitHub.Authentication;

/// <summary>
/// GitHub Authentication Aliases. Used mostly to get Jwt or Installation Tokens for GitHubs that need to make API Calls
/// </summary>
[CakeAliasCategory("GitHub")]
[CakeNamespaceImport("Cake.GitHub.Authentication")]
public static class GitHubAuthenticationAliases
{
    /// <summary>
    /// Generates an installation Token for a GitHub App that can be used to make API calls as an App
    /// </summary>
    /// <param name="context"></param>
    /// <param name="settings"></param>
    /// <returns>GitHub Access Token</returns>
    [CakeMethodAlias]
    public static AccessToken GitHubAppInstallationToken(this ICakeContext context, GitHubAppInstallationTokenToolSettings settings)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        CodeQL.Cli.CodeQLAddinInformation.LogVersionInformation(context.Log);

        var tool = new GitHubAppInstallationTokenTool(context);
        return tool.GenerateInstallationToken(settings);
    }
}
