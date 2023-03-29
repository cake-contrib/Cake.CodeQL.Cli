namespace Cake.CodeQL.Cli;

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
/// <summary>
/// Base class for all CodeQL cli tools.
/// </summary>
/// <typeparam name="TSettings">The settings type.</typeparam>
public abstract class CodeQLToolSettings : ToolSettings
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeQLToolSettings"/> class.
    /// </summary>
    /// <param name="command">Command to run.</param>
    protected CodeQLToolSettings(string command)
    {
        Command = command;
        RedirectStandardError = false;
        RedirectStandardOutput = false;
    }

    /// <summary>
    /// Gets or sets the process option to redirect standard error
    /// </summary>
    public bool RedirectStandardError { get; set; }

    /// <summary>
    /// Gets or sets the process option to redirect standard output
    /// </summary>
    public bool RedirectStandardOutput { get; set; }

    /// <summary>
    /// Gets or sets the Log level set by Cake.
    /// </summary>
    internal Verbosity? CakeVerbosityLevel { get; set; }

    /// <summary>
    /// Gets the command which should be run.
    /// </summary>
    protected string Command { get; private set; }

    /// <summary>
    /// Evaluates the settings and writes them to <paramref name="args"/>.
    /// </summary>
    /// <param name="args">The argument builder into which the settings should be written.</param>
    internal void Evaluate(ProcessArgumentBuilder args)
    {
        args.Append(Command);
        EvaluateCore(args);
    }

    /// <summary>
    /// Evaluates the settings and writes them to <paramref name="args"/>.
    /// </summary>
    /// <param name="args">The argument builder into which the settings should be written.</param>
    protected virtual void EvaluateCore(ProcessArgumentBuilder args)
    { }
}
