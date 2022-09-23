using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Tool;
using Cake.Core;
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
    /// Ef Core Tool Basic Settings
    /// </summary>
    public class EfCoreToolSettings : DotNetCoreSettings
    {
        /// <summary>
        ///  Filename of the DLL that contains your migrations.
        /// </summary>
        /// <example>My.Demo123.Api.dll</example>
        public string MigrationDll { get; set; }

        /// <summary>
        ///  .NET namespace for the DbContext class. 'Benday.Demo123.Api.dll
        /// </summary>
        /// <example>My.Demo123.Api</example>
        public string MigrationsNamespace { get; set; }

        /// <summary>
        /// Filename of the DLL that is the startup DLL for this published application
        /// </summary>
        /// <example>My.Demo123.WebUi.dll</example>
        public string StartupDll { get; set; }

        /// <summary>
        ///  Class name for the DbContext that controls the migrations to deploy
        /// </summary>
        /// <example>MyDbContext</example>
        public string DbContextClassName { get; set; }

        /// <summary>
        /// Gets or Sets whether to show Verbose output
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or Sets whether to show the version of Entity Framework.
        /// </summary>
        public bool Version { get; set; }

        /// <summary>
        /// Gets or Sets whether to colorize the output.
        /// </summary>
        public bool NoColor { get; set; }

        /// <summary>
        ///  Prefix output with level.
        /// </summary>
        public bool PrefixOutput { get; set; }
    }
}
