namespace Cake.CodeQL.Cli.Report;

/// <summary>
///  Tool for installing CodeQL Github Security Report cli on the host machine
/// </summary>
public class CodeQLReportInstallToolSettings
{
    /// <summary>
    /// The repository that contains the install download for, in &lt;owner&gt;/&lt;repository_name&gt;&lt; form.
    /// </summary>
    /// <remarks>
    /// <b>Possible Repositories: </b>
    /// <list type="bullet">
    /// <item>peter-murray/github-security-report-action</item>
    /// <item>jorge-abarca/github-security-report-action</item>
    /// </list>
    /// </remarks>
    public string Repository { get; set; } = "jorge-abarca/github-security-report-action";

    /// <summary>
    /// Gets or sets the working directory
    /// </summary>
    /// <value>The working directory.</value>
    public DirectoryPath WorkingDirectory { get; set; }

    /// <summary>
    /// Gets or sets the install directory that  CodeQL Github Security Report cli will be extracted to. Defaults to "./tools/codeql/reports"
    /// </summary>
    /// <value>the install directory that  CodeQL Github Security Report cli will be extracted tool.</value>
    public DirectoryPath InstallDirectory { get; set; }

    /// <summary>
    /// Version of the Github Security Report cli to  download. Defaults to v2. Must match a tag on the <seealso cref="CodeQLReportInstallToolSettings.Repository">repository</seealso> property.
    /// </summary>
    public string Version { get; set; } = "v2.0";

    /// <summary>
    /// Gets or Sets whether to force an install of CodeQL Github Security Report cli even if the tool is already installs
    /// </summary>
    public bool Force { get; set; } = false;
}
