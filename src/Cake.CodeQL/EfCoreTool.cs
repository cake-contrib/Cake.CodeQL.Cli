using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.EntityFrameworkCore
{
    /// <summary>
    /// Base class for all efcore tools.
    /// </summary>
    /// <typeparam name="TSettings">The settings type.</typeparam>
    public abstract class EfCoreTool<TSettings> : DotNetCoreTool<TSettings>
        where TSettings : EfCoreToolSettings
    {
        protected readonly ICakeEnvironment _environment;
        protected readonly ICakeLog _log;
        private readonly IToolLocator _tools;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfCoreTool" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        protected EfCoreTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
            _log = log;
            _tools = tools;
        }

        /// <summary>
        /// Adds specific tool commandline arguments.
        /// </summary>
        /// <param name="builder">Process argument builder to update.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Returns <see cref="ProcessArgumentBuilder"/> updated with common commandline arguments.</returns>
        protected abstract ProcessArgumentBuilder AppendToolArguments(ProcessArgumentBuilder builder, TSettings settings);

        /// <summary>
        /// Gets all the Arguments needed to run a ef command from a mgiration dll.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>Returns <see cref="ProcessArgumentBuilder"/> updated with common commandline arguments.</returns>
        protected ProcessArgumentBuilder GetArguments(TSettings settings)
        {
            var builder = base.CreateArgumentBuilder(settings);

            var migrationsDirectory = settings.WorkingDirectory.MakeAbsolute(_environment);
            var migrationDllPath = settings.WorkingDirectory.CombineWithFilePath(settings.MigrationDll).MakeAbsolute(_environment);
            var startupDllPath = settings.WorkingDirectory.CombineWithFilePath(settings.StartupDll).MakeAbsolute(_environment);
            var runtimeConfigPath = settings.WorkingDirectory.CombineWithFilePath(System.IO.Path.GetFileNameWithoutExtension(settings.StartupDll)).AppendExtension(".runtimeconfig.json").MakeAbsolute(_environment).MakeAbsolute(_environment);
            var depsJsonPath = settings.WorkingDirectory.CombineWithFilePath(System.IO.Path.GetFileNameWithoutExtension(settings.StartupDll)).AppendExtension(".deps.json").MakeAbsolute(_environment);
            var pathToNuGetPackages = DirectoryPath.FromString(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)).Combine(".nuget/packages").MakeAbsolute(_environment);
            var efDllPath = _tools.Resolve("ef.dll").MakeAbsolute(_environment);

            builder.Append("exec");
            builder.AppendSwitchQuoted("--depsfile", depsJsonPath.ToString());
            builder.AppendSwitchQuoted("--additionalprobingpath", pathToNuGetPackages.ToString());
            builder.AppendSwitchQuoted("--runtimeconfig", runtimeConfigPath.ToString());
            builder.AppendQuoted(efDllPath.ToString());

            AppendToolArguments(builder, settings);

            builder.AppendSwitchQuoted("--assembly", migrationDllPath.ToString());
            builder.AppendSwitchQuoted("--startup-assembly", startupDllPath.ToString());
            builder.AppendSwitchQuoted("--project-dir", migrationsDirectory.ToString());
            builder.AppendSwitchQuoted("--data-dir", migrationsDirectory.ToString());
            builder.AppendSwitchQuoted("--context", settings.DbContextClassName);

            if (!string.IsNullOrWhiteSpace(settings.MigrationsNamespace))
                builder.AppendSwitchQuoted("--root-namespace", settings.MigrationsNamespace);

            if (settings.Verbose)
                builder.Append("--verbose");

            return builder;

        }
    }
}