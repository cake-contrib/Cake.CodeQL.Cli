using Cake.CodeQL.Cli.Install;
using Cake.CodeQL.Cli.Report;

namespace Cake.CodeQL.Cli;

/// <summary>
/// CodeQL Cli aliases
/// </summary>
[CakeAliasCategory("CodeQL")]
[CakeNamespaceImport("Cake.CodeQL.Cli")]
public static class CodeQLAliases
{
    /// <summary>
    /// Creates a CodeQL Databases that can be use for vunerability scanning. Assumes the codeQL cli is installed on the host and available in the terminal path.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <example>
    /// <para> Creates a CodeQL database in a specific working directory.</para>
    /// <code>
    /// <![CDATA[
    ///    CodeQLCreateDatabase(new CodeQLCreateDatabaseToolSettings
    ///    {
    ///         DatabaseDir = "./tools/codeql-db",
    ///         WorkingDirectory = "./",
    ///         SourceRootDir = "./src",
    ///         BuildCommand = "dotnet build /t:rebuild",
    ///         NoRunUnnecessaryBuilds = true,
    ///         Languages = { CodeLanguage.csharp }
    ///    });
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory("Database")]
    public static void CodeQLCreateDatabase(this ICakeContext context, CodeQLCreateDatabaseToolSettings settings)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        CodeQLAddinInformation.LogVersionInformation(context.Log);
        var tool = new CodeQLCreateDatabaseTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log);
        tool.CreateDatabase(settings);
    }

    /// <summary>
    /// Analyzes a CodeQL database for vunerabilities and summarizes the results in a SARIF file. Assumes the codeQL cli is installed on the host and available in the terminal path.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <example>
    /// <para> Analyzes a CodeQL database and produces an output SARIF file</para>
    /// <code>
    /// <![CDATA[
    ///  //Example of C# compiled language
    ///
    ///  CodeQLAnalyzeDatabase(new CodeQLAnalyzeDatabaseToolSettings
    ///  {
    ///     DatabaseDir = "./tools/codeql-db",
    ///     WorkingDirectory = "./",
    ///     OutputFile = "./results/example-repo.sarif",
    ///     Threads = 1,
    ///     SarifCategory = "csharp",
    ///     Format = "sarifv2.1.0",
    ///     Verbose = true
    ///   });
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory("Database")]
    public static void CodeQLAnalyzeDatabase(this ICakeContext context, CodeQLAnalyzeDatabaseToolSettings settings)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        CodeQLAddinInformation.LogVersionInformation(context.Log);
        var tool = new CodeQLAnalyzeDatabaseTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log);
        tool.AnalyzeDatabase(settings);
    }

    /// <summary>
    /// Uploads the results of a CodeQL analysis in SARIF file format to GitHub or GitHub Enterprise Server. Assumes the codeQL cli is installed on the host and available in the terminal path.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <example>
    /// <para></para>
    /// <code>
    /// <![CDATA[
    ///  //Example of C# compiled language
    ///
    ///  CodeQLUploadResults(context, new CodeQLUploadResultsToolSettings
    ///  {
    ///    RepositoryName = "my-org/example-repo",
    ///    Ref = "refs/heads/main",
    ///    Commit = "deb275d2d5fe9a522a0b7bd8b6b6a1c939552718",
    ///    GitHubAuthStdin = true,
    ///    GitHubUrl = "https://github.mycompany.com",
    ///    SarifFilePath = "./results/example-repo.sarif",
    ///    WorkingDirectory = "./"
    ///  });
    ///
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory("Upload")]
    public static void CodeQLUploadResults(this ICakeContext context, CodeQLUploadResultsToolSettings settings)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        CodeQLAddinInformation.LogVersionInformation(context.Log);
        var tool = new CodeQLUploadResultsTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log);
        tool.UploadSarifFileResults(settings);
    }

    /// <summary>
    /// Installs CodeQL on the host agent.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <example>
    /// <para></para>
    /// <code>
    /// <![CDATA[
    ///  //Example of C# compiled language
    ///
    ///  CodeQLInstall(context, nnew CodeQLInstallToolSettings
   	///  {
    ///     Force = false,
    ///     WorkingDirectory = "./",
    ///     InstallDirectory = "./tools/codeql"
    ///  }/
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory("Install")]
    public static void CodeQLInstall(this ICakeContext context, CodeQLInstallToolSettings settings)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        CodeQLAddinInformation.LogVersionInformation(context.Log);
        var tool = new CodeQLInstallTool(context);
        tool.Install(settings);
    }

    /// <summary>
    /// Installs GitHub Advanced Security Code Scan Report cli on the host agent.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <example>
    /// <para></para>
    /// <code>
    /// <![CDATA[
    ///  //Example of C# compiled language
    ///
    ///  CodeQLReportInstall(context, nnew CodeQLReportInstallToolSettings
   	///  {
    ///     Force = false,
    ///     WorkingDirectory = "./",
    ///     InstallDirectory = "./tools/codeql/reports"
    ///  }/
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory("Reports")]
    public static void CodeQLReportInstall(this ICakeContext context, CodeQLReportInstallToolSettings settings)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        CodeQLAddinInformation.LogVersionInformation(context.Log);
        var tool = new CodeQLReportInstallTool(context);
        tool.Install(settings);
    }

    /// <summary>
    /// Generates an GitHub Advanced Security Code Scan Report
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    /// <example>
    /// <para></para>
    /// <code>
    /// <![CDATA[
    ///
    ///  CodeQLReportGenerate(context, nnew CodeQLReportInstallToolSettings
    ///  {
    ///     WorkingDirectory = "./"
    ///     GitHubToken = "ghp_xxxxxxxxxxxxxxxxxx"
    ///     Repository = "peter-murray/node-hue-api",
    ///     SarifReportDirectory = "../results",
    ///     OutputDirectory = "./",
    ///     GitHubApiUrl = "https://api.github.com"
    ///  }
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory("Reports")]
    public static void CodeQLReportGenerate(this ICakeContext context, CodeQLSecurityReportToolSettings settings)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        CodeQLAddinInformation.LogVersionInformation(context.Log);
        var tool = new CodeQLSecurityReportTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log);
        tool.GenerateReport(settings);
    }
}


