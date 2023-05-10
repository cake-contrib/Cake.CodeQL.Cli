namespace Cake.CodeQL.Cli;

internal class CodeQLResolveToolPath
{
    private readonly IFileSystem fileSystem;
    private readonly ICakeEnvironment environment;

    public CodeQLResolveToolPath(IFileSystem fileSystem, ICakeEnvironment environment)
    {
        this.fileSystem = fileSystem;
        this.environment = environment;
    }

    public IEnumerable<FilePath> Find(IEnumerable<string> toolNames, DirectoryPath dir)
    {
        if (dir == null || toolNames == null || toolNames?.Count() < 1) return null;

        var globSettings = new GlobberSettings()
        {
            IsCaseSensitive = false,
            FilePredicate = file => toolNames.Any(toolName => toolName.Equals(file.Path.GetFilename().ToString(), StringComparison.OrdinalIgnoreCase))
        };

        var globPattern = $"{dir.MakeAbsolute(environment).ToString().TrimEnd('/', '\\')}/**/*";

        var globber = new Globber(fileSystem, environment);

        return globber.Match(globPattern, globSettings).OfType<FilePath>();
    }
}
