namespace Cake.CodeQL.Cli.Database;

/// <summary>
/// Tool for analyzing CodeQL Databases that can be use for vunerability scanning
/// </summary>
public class CodeQLAnalyzeDatabaseTool : CodeQLTool<CodeQLAnalyzeDatabaseToolSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLAnalyzeDatabaseTool"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="environment">The environment.</param>
    /// <param name="processRunner">The process runner.</param>
    /// <param name="tools">The tool locator.</param>
    /// <param name="log">Cake log instance.</param>
    public CodeQLAnalyzeDatabaseTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log)
        : base(fileSystem, environment, processRunner, tools, log)
    { }

    /// <summary>
    ///  Run queries to analyze each CodeQL database and summarize the results in a SARIF file.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public void AnalyzeDatabase(CodeQLAnalyzeDatabaseToolSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        RunCore(settings);
    }
}
