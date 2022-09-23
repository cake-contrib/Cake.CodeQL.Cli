using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.EntityFrameworkCore.Tests
{
    internal abstract class EfCoreToolFixture<TSettings> : ToolFixture<TSettings, ToolFixtureResult>
        where TSettings : EfCoreToolSettings, new()
    {
        protected EfCoreToolFixture()
            : base("dotnet.exe")
        {
            ProcessRunner.Process.SetStandardOutput(new string[] { });
            Log.Verbosity = Verbosity.Normal;
        }

        protected override ToolFixtureResult CreateResult(FilePath path, ProcessSettings process) => new ToolFixtureResult(path, process);

        protected ICakeLog Log { get; } = new FakeLog();
    }
}
