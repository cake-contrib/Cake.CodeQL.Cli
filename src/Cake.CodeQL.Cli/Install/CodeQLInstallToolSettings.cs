namespace Cake.CodeQL.Cli.Install;

/// <summary>
///  Tool for installing CodeQL on the host machine
/// </summary>
public class CodeQLInstallToolSettings
{
    /// <summary>
    /// Gets or sets the working directory
    /// </summary>
    /// <value>The working directory.</value>
    public DirectoryPath WorkingDirectory { get; set; }

    /// <summary>
    /// Gets or sets the install directory that CodeQL will be extracted to. Defaults to "./tools/codeql"
    /// </summary>
    /// <value>the install directory that CodeQL will be extracted tool.</value>
    public DirectoryPath InstallDirectory { get; set; }

    /// <summary>
    /// Gets or Sets whether to force an install of CodeQL even if the tool is already installs
    /// </summary>
    public bool Force { get; set; } = false;
}
