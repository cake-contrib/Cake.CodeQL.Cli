using System;

namespace Cake.CodeQL.Cli.Report;

/// <summary>
/// Tool for generating a pdf summery of GitHub Security report
/// </summary
/// <example>
/// ./github-security-report.exe -t &lt;GitHub PAT Token&gt; -r peter-murray/node-hue-api -s &lt;directory containing CodeQL SARIF file(s)&gt;
/// </example>
public class CodeQLSecurityReportTool : Tool<CodeQLSecurityReportToolSettings>
{
    private readonly IFileSystem fileSystem;
    private readonly ICakeEnvironment environment;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLSecurityReportTool"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="environment">The environment.</param>
    /// <param name="processRunner">The process runner.</param>
    /// <param name="tools">The tool locator.</param>
    /// <param name="log">Cake log instance.</param>
    public CodeQLSecurityReportTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log)
        : base(fileSystem, environment, processRunner, tools)
    {
        this.fileSystem = fileSystem;
        this.environment = environment;
        this.environment = environment;
        this.fileSystem = fileSystem;
        CakeLog = log;
    }


    /// <summary>
    /// Cake log instance.
    /// </summary>
    public ICakeLog CakeLog { get; }

    /// <summary>
    /// Gets the possible names of the tool executable.
    /// </summary>
    /// <returns>The tool executable name.</returns>
    protected sealed override IEnumerable<string> GetToolExecutableNames() => new[]
    {
        "github-security-report",
        "github-security-report.exe",
        "github-security-report-mac-x64",
        "github-security-report-linux-x64"
    };


    protected override IEnumerable<FilePath> GetAlternativeToolPaths(CodeQLSecurityReportToolSettings settings)
    {
        var toolResolver = new CodeQLResolveToolPath(fileSystem, environment);

        var toolPaths = toolResolver.Find(GetToolExecutableNames(), settings.WorkingDirectory.Combine("tools"));

        if (toolPaths == null || toolPaths.Count() < 1)
            return base.GetAlternativeToolPaths(settings);

        return toolPaths;
    }

    /// <summary>
    /// Gets the name of the tool.
    /// </summary>
    /// <returns>The name of the tool.</returns>>
    protected override string GetToolName() => "GitHub Security Report";

    /// <summary>
    /// Generates an GitHub Advanced Security Code Scan Report
    /// </summary>
    /// <param name="settings">The settings.</param>
    public void GenerateReport(CodeQLSecurityReportToolSettings settings)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        RunCore(settings, new ProcessSettings(), null);
    }

    /// <summary>
    /// Runs GitHub Security Report.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <param name="processSettings">The process settings. <c>null</c> for default settings.</param>
    /// <param name="postAction">Action which should be executed after running libman. <c>null</c> for no action.</param>
    protected void RunCore(CodeQLSecurityReportToolSettings settings, ProcessSettings processSettings, Action<IProcess> postAction)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        if (!settings.CakeVerbosityLevel.HasValue)
            settings.CakeVerbosityLevel = CakeLog.Verbosity;

        processSettings.RedirectStandardError = settings.RedirectStandardError;
        processSettings.RedirectStandardOutput = settings.RedirectStandardOutput;

        var args = GetArguments(settings);
        Run(settings, args, processSettings, postAction);
    }

    /// <summary>
    /// Builds the arguments for npm.
    /// </summary>
    /// <param name="settings">Settings used for building the arguments.</param>
    /// <returns>Argument builder containing the arguments based on <paramref name="settings"/>.</returns>
    protected ProcessArgumentBuilder GetArguments(CodeQLSecurityReportToolSettings settings)
    {
        var args = new ProcessArgumentBuilder();
        settings.Evaluate(args);

        CakeLog.Verbose("GitHub Security Report arguments: {0}", args.RenderSafe());

        return args;
    }

}
