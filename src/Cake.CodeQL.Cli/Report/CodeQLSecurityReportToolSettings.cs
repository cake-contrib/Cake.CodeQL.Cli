namespace Cake.CodeQL.Cli.Report;

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
/// <summary>
/// Tool for generating report
/// </summary>
public class CodeQLSecurityReportToolSettings : ToolSettings
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
{
    /// <summary>
    ///  The GitHub Personal Access Token that has the necessary access for security and dependency API endpoints.
    /// </summary>
    public string GitHubToken { get; set; }

    /// <summary>
    /// The repository that contains the source code, in &lt;owner&gt;/&lt;repository_name&gt;&lt;owner&gt;/&lt;repository_name&gt; form, e.g. peter-murray/node-hue-api
    /// </summary>
    public string Repository { get; set; }

    /// <summary>
    /// The directory containing the SARIF report files. Defaults to "../results";
    /// </summary>
    public DirectoryPath SarifReportDirectory { get; set; }

    /// <summary>
    /// The directory to output the PDF report to. This will be created if it does not exist. Defaults to "./".
    /// </summary>
    public DirectoryPath OutputDirectory { get; set; }

    /// <summary>
    /// GitHub API URL. Defaults to "https://api.github.com"
    /// </summary>
    public string GitHubApiUrl { get; set; }

    /// <summary>
    /// The report template type used to render the report.
    /// </summary>
    public Template? Template { get; set; }

    /// <summary>
    /// Gets or sets the process option to redirect standard error
    /// </summary>
    public bool RedirectStandardError { get; set; }

    /// <summary>
    /// Gets or sets the process option to redirect standard output
    /// </summary>
    public bool RedirectStandardOutput { get; set; }

    /// <summary>
    /// Gets or sets the Log level set by Cake.
    /// </summary>
    internal Verbosity? CakeVerbosityLevel { get; set; }

    /// <summary>
    /// Evaluates the settings and writes them to <paramref name="args"/>.
    /// </summary>
    /// <param name="args">The argument builder into which the settings should be written.</param>
    internal void Evaluate(ProcessArgumentBuilder args)
    {
        const string separator = " ";

        if (string.IsNullOrEmpty(GitHubToken)) throw new ArgumentNullException(nameof(GitHubToken), $"Missing required {nameof(GitHubToken)} property.");
        if (string.IsNullOrWhiteSpace(Repository)) throw new ArgumentNullException(nameof(Repository), $"Missing required {nameof(Repository)} property.");

        args.AppendSwitchQuotedSecret("--token", separator, GitHubToken);
        args.AppendSwitchQuoted("--repository", separator, Repository);

        if(SarifReportDirectory != null)
            args.AppendSwitchQuoted("--sarif-directory", separator, SarifReportDirectory.FullPath);

        if(OutputDirectory != null)
            args.AppendSwitchQuoted("--output-directory", separator, OutputDirectory.FullPath);

        if (Template.HasValue)
            args.AppendSwitch("--template", Template.Value.ToString());

        if (!string.IsNullOrWhiteSpace(GitHubApiUrl))
            args.AppendSwitchQuoted("---github-api-url", separator, GitHubApiUrl);
    }
}