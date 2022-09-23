using Cake.Common.Tools.DotNetCore;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.EntityFrameworkCore.Database
{
    /// <summary>
    /// Contains settings used by <see cref="EfCoreDatabaseUpdateTool"/>.
    /// </summary>
    public class EfCoreDatabaseUpdateSettings : EfCoreToolSettings
    {
        /// <summary>
        /// The target migration. If '0', all migrations will be reverted. Defaults to the last migration.
        /// </summary>
        public string Migration { get; set; }
    }
}
