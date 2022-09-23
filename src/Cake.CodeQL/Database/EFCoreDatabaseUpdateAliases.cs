using Cake.Core;
using Cake.Core.Annotations;
using Cake.EntityFrameworkCore;
using Cake.EntityFrameworkCore.Database;
using System;
using System.Runtime;

namespace Cake.EntityFrameworkCore.Database
{
    /// <summary>
    /// Entity Framework Core (ef.dll) Database Update aliases
    /// </summary>
    [CakeAliasCategory("EntityFrameworkCore")]
    [CakeNamespaceImport("Cake.EntityFrameworkCore.Database")]
    public static class EFCoreDatabaseUpdateAliases
    {
        /// <summary>
        ///  Applies any pending migrations to the database using the ef.dll migration tool.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <example>
        /// <para> Applies any pending migrations to the database using the ef.dll dotnet tool.  ('dotnet ef database update')</para>
        /// <code>
        /// <![CDATA[
        ///    EfCoreDatabaseUpdate(context, settings =>
        ///    {
        ///         settings
        ///           .FromPath(@"c:/myproject")
        ///           .SetDbContextClass("MyDbContext")
        ///           .SetMigrationsDll("MyMigrations.dll")
        ///           .SetMigrationsNamespace("My.Demo.Migrations")
        ///           .SetStartupDll("MyWebUI.dll")
        ///           .SetTargetMigration("20201221_MyTable_Change")
        ///           .SetVersion()
        ///           .SetNoColor()
        ///           .SetVerbose();
        ///    });
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Database")]
        public static void EfCoreDatabaseUpdate(this ICakeContext context, Action<EfCoreDatabaseUpdateSettings> configurator)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new EfCoreDatabaseUpdateSettings();
            configurator(settings);
            context.EfCoreDatabaseUpdate(settings);
        }

        /// <summary>
        /// Applies any pending migrations to the database using the ef.dll migration tool.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <para> Applies any pending migrations to the database using the ef.dll migration tool.  ('dotnet ef database update')</para>
        /// <code>
        /// <![CDATA[
        ///     var settings = new EfCoreDatabaseUpdateSettings
        ///     {
        ///         WorkingDirectory = @"c:/myproject",
        ///         DbContextClassName = "MyDbContext",
        ///         MigrationsNamespace = "My.Demo.Migrations",
        ///         MigrationDll = "MyMigrations.dll",
        ///         StartupDll = "MyWebUI.dll",
        ///         Verbose = true,
        ///         Version = true
        ///     };
        ///      EfCoreDatabaseUpdate(settings);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Database")]
        public static void EfCoreDatabaseUpdate(this ICakeContext context, EfCoreDatabaseUpdateSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            EfCoreAddinInformation.LogVersionInformation(context.Log);
            var tool = new EfCoreDatabaseUpdater(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log);
            tool.Update(settings);
        }
    }
}