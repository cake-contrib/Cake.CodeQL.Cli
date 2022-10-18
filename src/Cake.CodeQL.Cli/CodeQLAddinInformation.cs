namespace Cake.CodeQL.Cli;

internal static class CodeQLAddinInformation
{
    private static readonly string InformationalVersion = typeof(CodeQLAddinInformation).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    private static readonly string AssemblyVersion = typeof(CodeQLAddinInformation).GetTypeInfo().Assembly.GetName().Version.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    private static readonly string AssemblyName = typeof(CodeQLAddinInformation).GetTypeInfo().Assembly.GetName().Name;

    /// <summary>
    /// verbosely log addin version information
    /// </summary>
    /// <param name="log"></param>
    public static void LogVersionInformation(ICakeLog log) => log.Verbose(entry => entry("Using addin: {0} v{1} ({2})", AssemblyName, AssemblyVersion, InformationalVersion));
}
