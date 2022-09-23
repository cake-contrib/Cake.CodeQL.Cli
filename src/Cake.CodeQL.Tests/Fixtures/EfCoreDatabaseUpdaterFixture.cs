using Cake.EntityFrameworkCore.Database;

namespace Cake.EntityFrameworkCore.Tests.Database
{
    internal sealed class EfCoreDatabaseUpdaterFixture : EfCoreToolFixture<EfCoreDatabaseUpdateSettings>
    {
        protected override void RunTool()
        {
            var tool = new EfCoreDatabaseUpdater(FileSystem, Environment, ProcessRunner, Tools, Log);
            tool.Update(Settings);
        }
    }
}
