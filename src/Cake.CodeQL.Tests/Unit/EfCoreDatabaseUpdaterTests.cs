using Cake.Core.IO;
using System;
using Xunit;

namespace Cake.EntityFrameworkCore.Tests.Database
{
    public sealed class EfCoreDatabaseUpdaterTests
    {
        public sealed class TheUpdateMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new EfCoreDatabaseUpdaterFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Update()
            {
                // Given
                var fixture = new EfCoreDatabaseUpdaterFixture();

                fixture.Tools.RegisterFile("c:/myproject/tools/Microsoft.EntityFrameworkCore.Tools.5.0.4/netcoreapp2.0/any/ef.dll");

                fixture.Settings.WorkingDirectory = "c:/myproject";
                fixture.Settings.DbContextClassName = "MyDbContext";
                fixture.Settings.MigrationsNamespace = "My.Demo.Migrations";
                fixture.Settings.MigrationDll = "MyNamespace.Migrations.dll";
                fixture.Settings.StartupDll = "MyNamespace.WebUI.dll";
                fixture.Settings.Verbose = true;
                fixture.Settings.Version = true;

                // When
                var result = fixture.Run();

                // Then
                var expected = $"exec --depsfile \"c:/myproject/MyNamespace.WebUI.deps.json\" --additionalprobingpath \"{DirectoryPath.FromString(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))}/.nuget/packages\" --runtimeconfig \"c:/myproject/MyNamespace.Migrations.runtimeconfig.json\" \"c:/myproject/tools/Microsoft.EntityFrameworkCore.Tools.5.0.4/netcoreapp2.0/any/ef.dll\" database update --assembly \"c:/myproject/MyNamespace.Migrations.dll\" --startup-assembly \"c:/myproject/MyNamespace.WebUI.dll\" --project-dir \"c:/myproject\" --data-dir \"c:/myproject\" --context \"MyDbContext\" --root-namespace \"My.Demo.Migrations\" --verbose";

                Assert.Equal(expected, result.Args);
            }
        }
    }
}
