using Cake.Common.Tools.Command;
using System;

namespace Cake.CodeQL.Cli.Install;

/// <summary>
/// Tool for downloading and installing CodeQL
/// </summary>
public class CodeQLInstallTool
{
    private readonly ICakeContext _context;
    private readonly ICollection<string> _executableNames = new[] { "codeql.exe", "codeql", };

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

            // 2. Ensure out file not exits
            try { if (_context.FileExists(outFilePath)) _context.DeleteFile(outFilePath); } catch { }

            // 2. Download CodeQL
            _context.DownloadFile(downloadFileUri, outFilePath);

            // 3. Installs the CodeQL CLI on the Host Operating System.
            ExtractCodeQL(outFilePath, installDir);

            // 4. Ensure File is removed
            try { if (_context.FileExists(outFilePath)) _context.DeleteFile(outFilePath); } catch { }
        }

        // 5. Verifies that the CodeQL CLI is correctly set up to create and analyze databases.
        VerifyCodeQL(settings);
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

    private void VerifyCodeQL(CodeQLInstallToolSettings settings)
    {
        var toolResolver = new CodeQLResolveToolPath(_context.FileSystem, _context.Environment);
        var toolPaths = toolResolver.Find(_executableNames, settings.WorkingDirectory);

        var exePath = toolPaths.FirstOrDefault(tp => _context.IsRunningOnWindows() && tp.HasExtension);

        _context.Command
        (
            toolExecutableNames: _executableNames,
            arguments: "resolve qlpacks",
            settingsCustomization: cmdSettings =>
            {
                cmdSettings.ToolPath = exePath;
                return cmdSettings;
            }
        );
    }

    private DirectoryPath GetInstallDirectory(CodeQLInstallToolSettings settings) =>
        settings.InstallDirectory != null ? _context.MakeAbsolute(settings.InstallDirectory) : _context.MakeAbsolute(GetWorkingDirectory(settings).Combine("tools/codeql"));

    private DirectoryPath GetWorkingDirectory(CodeQLInstallToolSettings settings) =>
        settings.WorkingDirectory != null ? settings.WorkingDirectory : _context.Environment.WorkingDirectory;

    private FilePath GetCodeQLFilePath(CodeQLInstallToolSettings settings) =>
        _context.MakeAbsolute(_context.Directory(System.IO.Path.GetTempPath()))
               .CombineWithFilePath(_context.File(System.IO.Path.GetFileName(_context.Environment.Platform.Family switch
               {
                   PlatformFamily.Linux => Downloads.Linux,
                   PlatformFamily.Windows => Downloads.Windows,
                   PlatformFamily.OSX => Downloads.OSX,
                   _ => throw new NotSupportedException("Operating System is not supported.")
               })));

    private string GetCodeQLDownloadUrl() => _context.Environment.Platform.Family switch
    {
        PlatformFamily.Linux => Downloads.Linux,
        PlatformFamily.Windows => Downloads.Windows,
        PlatformFamily.OSX => Downloads.OSX,
        _ => throw new NotSupportedException("Operating System is not supported.")
    };

    private class Downloads
    {
        public const string Linux = "https://github.com/github/codeql-action/releases/latest/download/codeql-bundle-linux64.tar.gz";
        public const string Windows = "https://github.com/github/codeql-action/releases/latest/download/codeql-bundle-win64.tar.gz";
        public const string OSX = "https://github.com/github/codeql-action/releases/latest/download/codeql-bundle-osx64.tar.gz";
    }
}
