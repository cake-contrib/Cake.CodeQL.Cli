namespace Cake.CodeQL.Cli.Upload;

/// <summary>
/// Tool for uploading results to GitHub
/// </summary>
public class CodeQLUploadResultsToolSettings : CodeQLToolSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLUploadResultsToolSettings"/>
    /// </summary>
    /// <remarks>
    /// codeql github upload-results --repository=&lt;repository-name&gt; --ref=&lt;ref&gt; --commit=&lt;commit&gt; --sarif=&lt;file&gt; --github-url=&lt;URL&gt; --github-auth-stdin
    /// </remarks>
    public CodeQLUploadResultsToolSettings()
        : base("github upload-results")
    { }

    /// <summary>
    /// Specify the OWNER/NAME of the repository to upload data to. The owner must be an organization within an enterprise that has a license for GitHub Advanced Security and GitHub Advanced Security must be enabled for the repository.
    /// </summary>
    public string RepositoryName { get; set; }

    /// <summary>
    /// Specify the name of the ref you checked out and analyzed so that the results can be matched to the correct code. For a branch use: refs/heads/BRANCH-NAME, for the head commit of a pull request use refs/pull/NUMBER/head, or for the GitHub-generated merge commit of a pull request use refs/pull/NUMBER/merge.
    /// </summary>
    public string Ref { get; set; }

    /// <summary>
    /// Specify the full SHA of the commit you analyzed.
    /// </summary>
    public string Commit { get; set; }

    /// <summary>
    /// Specify the SARIF file to load.
    /// </summary>
    public FilePath SarifFilePath { get; set; }

    /// <summary>
    /// Specify the URL for GitHub Enterprise Server. If blanks,defaults to to github.com.
    /// </summary>
    public string GitHubUrl { get; set; }

    /// <summary>
    /// Optional. Use to pass the CLI the GitHub App or personal access token created for authentication with GitHub's REST API via standard input. This is not needed if the command has access to a GITHUB_TOKEN environment variable set with this token.
    /// </summary>
    public bool GitHubAuthStdin { get; set; } = false;

    /// <summary>
    /// Evaluates the settings and writes them to <paramref name="args"/>.
    /// </summary>
    /// <param name="args">The argument builder into which the settings should be written.</param>
    protected override void EvaluateCore(ProcessArgumentBuilder args)
    {
        const string separator = "=";

        base.EvaluateCore(args);

        if (string.IsNullOrWhiteSpace(RepositoryName)) throw new ArgumentNullException(nameof(RepositoryName), $"Missing required {nameof(RepositoryName)} property.");
        if (string.IsNullOrWhiteSpace(Ref)) throw new ArgumentNullException(nameof(RepositoryName), $"Missing required {nameof(Ref)} property.");
        if (string.IsNullOrWhiteSpace(Commit)) throw new ArgumentNullException(nameof(Commit), $"Missing required {nameof(Commit)} property.");
        if (SarifFilePath == null) throw new ArgumentException(nameof(SarifFilePath), $"Missing required {nameof(SarifFilePath)} property.");

        args.AppendSwitch("--repository", separator, RepositoryName);
        args.AppendSwitch("--ref", separator, Ref);
        args.AppendSwitch("--commit", separator, Commit);
        args.AppendSwitchQuoted("--sarif", separator, SarifFilePath.FullPath);

        if (!string.IsNullOrWhiteSpace(GitHubUrl))
            args.AppendSwitchQuoted("--github-url", separator, GitHubUrl);

        if (GitHubAuthStdin)
            args.Append("--github-auth-stdin");
    }
}
