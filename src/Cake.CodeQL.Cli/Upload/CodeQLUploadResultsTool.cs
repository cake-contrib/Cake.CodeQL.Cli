namespace Cake.CodeQL.Cli.Upload;

/// <summary>
/// Tool for uploading CodeQL Sarif result(s) to github
/// </summary>
public class CodeQLUploadResultsTool : CodeQLTool<CodeQLUploadResultsToolSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLUploadResultsTool"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="environment">The environment.</param>
    /// <param name="processRunner">The process runner.</param>
    /// <param name="tools">The tool locator.</param>
    /// <param name="log">Cake log instance.</param>
    public CodeQLUploadResultsTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log)
        : base(fileSystem, environment, processRunner, tools, log)
    { }

    /// <summary>
    /// Uploads the sarif results of a CodeQL result to GitHub Server or GitHub
    /// </summary>
    /// <param name="settings">The settings.</param>
    public void UploadSarifFileResults(CodeQLUploadResultsToolSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        RunCore(settings);
    }
}
