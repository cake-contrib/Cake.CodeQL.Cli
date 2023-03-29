namespace Cake.CodeQL.Cli.Report;

/// <summary>
/// Tool for downloading and installing  CodeQL Github Security Report cli
/// </summary>
public class CodeQLReportInstallTool
{
    private readonly ICakeContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLReportInstallTool"/> class.
    /// </summary>
    /// <param name="cakeContext">The cake Context</param>
    public CodeQLReportInstallTool(ICakeContext cakeContext) => _context = cakeContext;

    /// <summary>
    /// Downloads CodeQL and extracts bundle on the agent machine
    /// </summary>
    /// <param name="settings">The settings.</param>
    public void Install(CodeQLReportInstallToolSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        // 1. Enusre CodeQL install status
        var installDir = GetInstallDirectory(settings);
        var dirExists = EnsureInstallDirectoryExists(settings, installDir);

        if (!dirExists)
        {
            var downloadFileUri = GetCodeQLReportCliDownloadUrl(settings);
            var outFilePath = GetCodeQLReportCliFilePath(settings);

            // 2. Ensure out file not exits
            try { if (_context.FileExists(outFilePath)) _context.DeleteFile(outFilePath); } catch { }

            // 3. Download CodeQL
            _context.DownloadFile(downloadFileUri, outFilePath);

            // 4. Installs the GitHub Advanced Security Code Scan Report CLI on the Host Operating System.
            ExtractCodeQLReportCli(outFilePath, installDir);

            // 5. Ensure File is removed
            try { if (_context.FileExists(outFilePath)) _context.DeleteFile(outFilePath); } catch { }
        }
    }

    /// <summary>
    /// Ensures the GitHub Advanced Security Code Scan Report directory exists....if Force set to true. This will always return false.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="installDir"></param>
    /// <returns></returns>
    private bool EnsureInstallDirectoryExists(CodeQLReportInstallToolSettings settings, DirectoryPath installDir)
    {
        var dirExists = _context.DirectoryExists(installDir);

        if (dirExists)
        {
            _context.Log.Verbose("GitHub Advanced Security Code Scan Report install {0} directory already exists.", installDir.FullPath);
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

    private void ExtractCodeQLReportCli(FilePath outFilePath, DirectoryPath installDir)
    {
        var tempExtractDir = _context.Directory(System.IO.Path.GetTempPath());
        var zipFile = tempExtractDir.Path.CombineWithFilePath(ZipFiles.GitHubSecurityReport);

        if (_context.FileExists(zipFile))
            _context.DeleteFile(zipFile);

        _context.Unzip(outFilePath, tempExtractDir);
        _context.Unzip(zipFile, installDir);
    }

    private DirectoryPath GetInstallDirectory(CodeQLReportInstallToolSettings settings) =>
        settings.InstallDirectory != null ? _context.MakeAbsolute(settings.InstallDirectory) : _context.MakeAbsolute(GetWorkingDirectory(settings).Combine("tools/codeql/reports"));

    private DirectoryPath GetWorkingDirectory(CodeQLReportInstallToolSettings settings) =>
        settings.WorkingDirectory != null ? settings.WorkingDirectory : _context.Environment.WorkingDirectory;

    private FilePath GetCodeQLReportCliFilePath(CodeQLReportInstallToolSettings settings) =>
        _context.MakeAbsolute(_context.Directory(System.IO.Path.GetTempPath()))
               .CombineWithFilePath(_context.File(System.IO.Path.GetFileName(_context.Environment.Platform.Family switch
               {
                   PlatformFamily.Linux => Downloads.Linux,
                   PlatformFamily.Windows => Downloads.Windows,
                   PlatformFamily.OSX => Downloads.OSX,
                   _ => throw new NotSupportedException("Operating System is not supported.")
               })));

    private string GetCodeQLReportCliDownloadUrl(CodeQLReportInstallToolSettings settings) => _context.Environment.Platform.Family switch
    {
        PlatformFamily.Linux => string.Format(Downloads.Linux, settings.Repository, settings.Version),
        PlatformFamily.Windows => string.Format(Downloads.Windows, settings.Repository, settings.Version),
        PlatformFamily.OSX => string.Format(Downloads.OSX, settings.Repository, settings.Version),
        _ => throw new NotSupportedException("Operating System is not supported.")
    };

    private class ZipFiles
    {
        public const string GitHubSecurityReport = "github-security-report-bundle.zip";
    }

    private class Downloads
    {
        public const string Linux = "https://github.com/{0}/releases/download/{1}/github-security-report-bundle-linux-x64.zip";
        public const string Windows = "https://github.com/{0}/releases/download/{1}/github-security-report-bundle-windows-x64.zip";
        public const string OSX = "https://github.com/{0}/releases/download/{1}/github-security-report-bundle-mac-x64.zip";
    }
}
