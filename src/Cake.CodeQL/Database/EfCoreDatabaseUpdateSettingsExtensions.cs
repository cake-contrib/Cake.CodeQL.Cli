using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.EntityFrameworkCore.Database
{
    /// <summary>
    /// Extensions for <see cref="EfCoreToolSettingsExtensions"/>.
    /// </summary>
    public static class EfCoreDatabaseUpdateSettingsExtensions
    {
        /// <summary>
        /// Sets a named target migration to migrate the database too.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="migration">The name of the migration to target</param>
        /// <returns>The <paramref name="settings"/> instance with <see cref="EfCoreDatabaseUpdateSettings.Migration"/> set to <paramref name="migration"/>.</returns>
        public static EfCoreDatabaseUpdateSettings SetTargetMigration(this EfCoreDatabaseUpdateSettings settings, string migration)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.Migration = migration;

            return settings;
        }
    }
}
