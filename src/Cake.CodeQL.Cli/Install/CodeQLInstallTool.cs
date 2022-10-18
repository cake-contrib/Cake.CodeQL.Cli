namespace Cake.CodeQL.Cli.Install;

/// <summary>
/// Tool for downloading and installing CodeQL
/// </summary>
public class CodeQLInstallTool
{
    private readonly ICakeContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLInstallTool"/> class.
    /// </summary>
    /// <param name="cakeContext">The cake Context</param>
    public CodeQLInstallTool(ICakeContext cakeContext) => _context = cakeContext;

    /// <summary>
    /// Downloads CodeQL and extracts bundle on the agent machine
    /// </summary>
    /// <param name="settings">The settings.</param>
    public void Install(CodeQLInstallToolSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        // 1. Enusre CodeQL install status
        var installDir = GetInstallDirectory(settings);
        var dirExists = EnsureInstallDirectoryExists(settings, installDir);

        if (!dirExists)
        {
            var downloadFileUri = GetCodeQLDownloadUrl();
            var outFilePath = GetCodeQLFilePath(settings);

            // 2. Download CodeQL
            _context.DownloadFile(downloadFileUri, outFilePath);

            // 3. Installs the CodeQL CLI on the Host Operating System.
            ExtractCodeQL(outFilePath, installDir);
        }

        // 4. Verifies that the CodeQL CLI is correctly set up to create and analyze databases.
        VerifyCodeQL();
    }

    /// <summary>
    /// Ensures the codeql directory exists....if Force set to true. This will always return false.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="installDir"></param>
    /// <returns></returns>
    private bool EnsureInstallDirectoryExists(CodeQLInstallToolSettings settings, DirectoryPath installDir)
    {
        var dirExists = _context.DirectoryExists(installDir);

        if (dirExists)
        {
            _context.Log.Verbose("CodeQL install {0} directory already exists.", installDir.FullPath);
            if (settings.Force)
            {
                _context.Log.Information("Force install requested. Deleting existing install directory {0}", installDir.FullPath);
                _context.EnsureDirectoryDoesNotExist(installDir);
                dirExists = false;
            }
            else
                return true;
        }

        _context.EnsureDirectoryExists(installDir);

        return dirExists;
    }

    private void ExtractCodeQL(FilePath outFilePath, DirectoryPath installDir)
    {
        var argBuilder = new ProcessArgumentBuilder();

        argBuilder.Append("-xvzf");
        argBuilder.AppendQuoted(outFilePath.FullPath);
        argBuilder.Append("-C");
        argBuilder.AppendQuoted(installDir.FullPath);

        _context.StartProcess("tar", new ProcessSettings { Arguments = argBuilder });
    }

    private void VerifyCodeQL() => _context.Command(toolExecutableNames: new[] { "codeql", "codeql.exe" }, arguments: "resolve qlpacks");

    private DirectoryPath GetInstallDirectory(CodeQLInstallToolSettings settings) =>
        settings.InstallDirectory != null ? _context.MakeAbsolute(settings.InstallDirectory) : _context.MakeAbsolute(GetWorkingDirectory(settings).Combine("tools/codeql"));

    private DirectoryPath GetWorkingDirectory(CodeQLInstallToolSettings settings) =>
        settings.WorkingDirectory != null ? settings.WorkingDirectory : _context.Environment.WorkingDirectory;

    private FilePath GetCodeQLFilePath(CodeQLInstallToolSettings settings)
    {
        var workingDir = GetWorkingDirectory(settings);
        var downloadFile = GetCodeQLDownloadUrl();
        var filePath = _context.File(System.IO.Path.GetFileName(downloadFile));

        return workingDir.CombineWithFilePath(filePath);
    }

    private string GetCodeQLDownloadUrl() => _context.Environment.Platform.Family switch
    {
        PlatformFamily.Linux => "https://github.com/github/codeql-action/releases/latest/download/codeql-bundle-linux64.tar.gz",
        PlatformFamily.Windows => "https://github.com/github/codeql-action/releases/latest/download/codeql-bundle-win64.tar.gz",
        PlatformFamily.OSX => "https://github.com/github/codeql-action/releases/latest/download/codeql-bundle-osx64.tar.gz",
        _ => throw new NotSupportedException("Operating System is not supported.")
    };
}
