namespace Cake.CodeQL.Cli.Database;

/// <summary>
/// Tool for create CodeQL Databases that can be use for vunerability scanning
/// </summary>
public class CodeQLCreateDatabaseTool : CodeQLTool<CodeQLCreateDatabaseToolSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLCreateDatabaseTool"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="environment">The environment.</param>
    /// <param name="processRunner">The process runner.</param>
    /// <param name="tools">The tool locator.</param>
    /// <param name="log">Cake log instance.</param>
    public CodeQLCreateDatabaseTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log)
        : base(fileSystem, environment, processRunner, tools, log)
    { }

    /// <summary>
    ///  Create a CodeQL database to represent the hierarchical structure of each supported programming language in the repository.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public void CreateDatabase(CodeQLCreateDatabaseToolSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        RunCore(settings);
    }
}
