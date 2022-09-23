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
    /// Extensions for <see cref="EfCoreToolSettingsExtensions"/>.
    /// </summary>
    public static class EfCoreToolSettingsExtensions
    {
        /// <summary>
        /// Sets the verbose output for Entity Framework output. (e.g. - Show verbose output).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.Verbose"/> set to true.</returns>
        public static T SetVerbose<T>(this T settings)
            where T : EfCoreToolSettings
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.Verbose = true;

            return settings;
        }

        /// <summary>
        /// Sets the no color output for Entity Framework output (e.g. - Don't colorize output).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.NoColor"/> set to true.</returns>
        public static T SetNoColor<T>(this T settings)
                    where T : EfCoreToolSettings
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.NoColor = true;

            return settings;
        }

        /// <summary>
        /// Sets the Prefix Out with level for the ef.dll. (e.g. - Prefix output with level.).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.PrefixOutput"/> set to true.</returns>
        public static T SetPrefixOutput<T>(this T settings)
         where T : EfCoreToolSettings
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.PrefixOutput = true;

            return settings;
        }

        /// <summary>
        /// Sets the to display version information in console output
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.Version"/> set to true.</returns>
        public static T SetVersion<T>(this T settings)
                    where T : EfCoreToolSettings
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.Version = true;

            return settings;
        }

        /// <summary>
        /// Sets the working directory which should be used to run the ef.dll command.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="path">Working directory which should be used to run the ef.dll command.</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="Core.Tooling.ToolSettings.WorkingDirectory"/> set to <paramref name="path"/>.</returns>
        public static T FromPath<T>(this T settings, DirectoryPath path)
            where T : EfCoreToolSettings
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.WorkingDirectory = path ?? throw new ArgumentNullException(nameof(path));

            return settings;
        }

        /// <summary>
        /// Sets the name of File of the DLL that contains your migrations
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="fileName">file name</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.MigrationDll"/>
        public static T SetMigrationsDll<T>(this T settings, string fileName)
            where T : EfCoreToolSettings
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            settings.MigrationDll = fileName ?? throw new ArgumentNullException(nameof(fileName));
            return settings;
        }

        /// <summary>
        /// Sets the migrations namespace that contains the DbContext
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="namespace">file name</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.MigrationsNamespace"/> set to <paramref name="namespace"/>.
        public static T SetMigrationsNamespace<T>(this T settings, string @namespace)
            where T : EfCoreToolSettings
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            settings.MigrationsNamespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            return settings;
        }

        /// <summary>
        /// Sets the statup dll for the published application the publised application
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="fileName">file name.</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.StartupDll"/> set to <paramref name="fileName"/>.
        public static T SetStartupDll<T>(this T settings, string fileName)
            where T : EfCoreToolSettings
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            settings.StartupDll = fileName ?? throw new ArgumentNullException(nameof(fileName));
            return settings;
        }

        /// <summary>
        /// Class name for the DbContext that controls the migrations to deploy
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="name">DbContext class name</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreToolSettings.DbContextClassName"/> set to <paramref name="name"/>.
        public static T SetDbContextClass<T>(this T settings, string name)
            where T : EfCoreToolSettings
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            settings.DbContextClassName = name ?? throw new ArgumentNullException(nameof(name));
            return settings;
        }
    }
}
