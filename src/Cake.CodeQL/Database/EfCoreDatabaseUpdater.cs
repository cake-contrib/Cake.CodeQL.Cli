using Cake.Common.Tools.DotNetCore;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Cake.EntityFrameworkCore.Database
{
    /// <summary>
    /// .Net Ef Core Database Updater using Migrations
    /// </summary>
    public class EfCoreDatabaseUpdater : EfCoreTool<EfCoreDatabaseUpdateSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfCoreDatabaseUpdater" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public EfCoreDatabaseUpdater(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log)
            : base(fileSystem, environment, processRunner, tools, log)
        { }


        /// <summary>
        ///  Updates the database to a specified migration.
        /// </summary>
        /// <param name="migration">The target migration. If '0', all migrations will be reverted. Defaults to the last migration..</param>
        /// <param name="settings">The settings.</param>
        public void Update(EfCoreDatabaseUpdateSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            RunCommand(settings, GetArguments(settings));
        }

        protected override ProcessArgumentBuilder AppendToolArguments(ProcessArgumentBuilder builder, EfCoreDatabaseUpdateSettings settings)
        {
            builder.Append("database update");

            if (!string.IsNullOrEmpty(settings.Migration))
                builder.Append(settings.Migration);

            return builder;
        }
    }
}