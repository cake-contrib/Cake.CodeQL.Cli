using Cake.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cake.EntityFrameworkCore
{
    internal static class EfCoreAddinInformation
    {
        private static readonly string InformationalVersion = typeof(EfCoreAddinInformation).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        private static readonly string AssemblyVersion = typeof(EfCoreAddinInformation).GetTypeInfo().Assembly.GetName().Version.ToString();
        private static readonly string AssemblyName = typeof(EfCoreAddinInformation).GetTypeInfo().Assembly.GetName().Name;

        /// <summary>
        /// verbosely log addin version information
        /// </summary>
        /// <param name="log"></param>
        public static void LogVersionInformation(ICakeLog log)
        {
            log.Verbose(entry => entry("Using addin: {0} v{1} ({2})", AssemblyName, AssemblyVersion, InformationalVersion));
        }
    }
}
