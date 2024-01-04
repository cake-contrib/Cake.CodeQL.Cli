namespace Cake.CodeQL.Cli.Database;

/// <summary>
/// Tool for creating CodeQL databases
/// </summary>
public class CodeQLCreateDatabaseToolSettings : CodeQLToolSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLCreateDatabaseToolSettings"/>
    /// </summary>
    /// <remarks>
    /// codeql database create /codeql-dbs/example-repo --language=javascript --source-root /checkouts/example-repo
    /// </remarks>
    public CodeQLCreateDatabaseToolSettings ()
        : base("database create")
    { }

    /// <summary>
    /// Specify the location of a directory to create for the CodeQL database. The command will fail if you try to overwrite an existing directory. If you also specify --db-cluster, this is the parent directory and a subdirectory is created for each language analyzed.
    /// </summary>
    /// <remarks>This is required</remarks>
    public DirectoryPath DatabaseDir { get; set; }

    /// <summary>
    /// One or more <seealso cref="CodeLanguage">CodeLanguage</seealso> to create CodeQL Database for.
    /// Specify the identifier for the language to create a database for, one of: cpp`, `csharp`, `go`, `java`, `javascript`, `python`, and `ruby (use javascript to analyze TypeScript code). When used with --db-cluster, the option accepts a comma-separated list, or can be specified more than once.
    /// </summary>
    /// <remarks>This is required</remarks>
    public IList<CodeLanguage> Languages { get; set; } = new List<CodeLanguage>();

    /// <summary>
    ///Recommended. Use to specify the build command or script that invokes the build process for the codebase. Commands are run from the current folder or, where it is defined, from --source-root. Not needed for Python and JavaScript/TypeScript analysis.
    /// </summary>
    public string BuildCommand { get; set; }

    /// <summary>
    /// Optional. Use in multi-language codebases to generate one database for each language specified.
    /// </summary>
    public bool DbCluster { get; set; }

    /// <summary>
    /// Recommended. Use to suppress the build command for languages where the CodeQL CLI does not need to monitor the build (for example, Python and JavaScript/TypeScript).
    /// </summary>
    public bool NoRunUnnecessaryBuilds { get; set; }

    /// <summary>
    /// Optional. Use if you run the CLI outside the checkout root of the repository. By default, the database create command assumes that the current directory is the root directory for the source files, use this option to specify a different location.
    /// </summary>
    public DirectoryPath SourceRootDir { get; set; }

    /// <summary>
    /// Advanced. Optional (Advanced). Use if you have a configuration file that specifies how to create the CodeQL databases and what queries to run in later steps.
    /// </summary>
    public FilePath CodeScanningConfigFile { get; set; }

    /// <summary>
    /// Evaluates the settings and writes them to <paramref name="args"/>.
    /// </summary>
    /// <param name="args">The argument builder into which the settings should be written.</param>
    protected override void EvaluateCore(ProcessArgumentBuilder args)
    {
        const string separator = "=";

        base.EvaluateCore(args);

        if (DatabaseDir == null) throw new ArgumentNullException(nameof(DatabaseDir), $"Missing required {nameof(DatabaseDir)} property.");
        if (Languages is null || !Languages.Any()) throw new ArgumentException(nameof(Languages), $"At least one coding language is required to be specified.");

        args.AppendQuoted(DatabaseDir.FullPath);

        args.AppendSwitch("--language", separator, string.Join(',', Languages));

        if (!string.IsNullOrWhiteSpace(BuildCommand))
        {
            if (BuildCommand.Contains(separator))
                args.AppendSwitchQuoted("--command", separator, BuildCommand);
            else
                args.AppendSwitch("--command", separator, BuildCommand);
        }

        if (DbCluster)
            args.Append("--db-cluster");

        if (NoRunUnnecessaryBuilds)
            args.Append("--no-run-unnecessary-builds");

        if (SourceRootDir != null)
            args.AppendSwitchQuoted("--source-root", separator, SourceRootDir.FullPath);

        if (CodeScanningConfigFile != null)
            args.AppendSwitchQuoted("--codescanning-config", separator, CodeScanningConfigFile.FullPath);
    }
}
