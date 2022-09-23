using Shouldly;
using Xunit;

namespace Cake.EntityFrameworkCore.Tests
{
    public sealed class EfCoreToolSettingsExtensionsTests
    {
        public sealed class TheSetVerboseMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                EfCoreToolSettings settings = null;

                // When
                var result = Record.Exception(() => settings.SetVerbose());

                // Then
                result.IsArgumentNullException(nameof(settings));
            }

            [Fact]
            public void Should_Set_Verbose_To_True()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetVerbose();

                // Then
                settings.Verbose.ShouldBeTrue();
            }
        }

        public sealed class TheSetNoColorMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                EfCoreToolSettings settings = null;

                // When
                var result = Record.Exception(() => settings.SetNoColor());

                // Then
                result.IsArgumentNullException(nameof(settings));
            }

            [Fact]
            public void Should_Set_NoColor_To_True()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetNoColor();

                // Then
                settings.NoColor.ShouldBeTrue();
            }
        }

        public sealed class TheSetPrefixOutputMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                EfCoreToolSettings settings = null;

                // When
                var result = Record.Exception(() => settings.SetPrefixOutput());

                // Then
                result.IsArgumentNullException(nameof(settings));
            }

            [Fact]
            public void Should_Set_PrefixOutput_To_True()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetPrefixOutput();

                // Then
                settings.PrefixOutput.ShouldBeTrue();
            }
        }

        public sealed class TheSetVersionMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                EfCoreToolSettings settings = null;

                // When
                var result = Record.Exception(() => settings.SetVersion());

                // Then
                result.IsArgumentNullException(nameof(settings));
            }

            [Fact]
            public void Should_Set_Version_To_True()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetVersion();

                // Then
                settings.Version.ShouldBeTrue();
            }

        }

        public sealed class TheFromPathMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                EfCoreToolSettings settings = null;

                // When
                var result = Record.Exception(() => settings.FromPath(@"c:\temp"));

                // Then
                result.IsArgumentNullException(nameof(settings));
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Null()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                var result = Record.Exception(() => settings.FromPath(null));

                // Then
                result.IsArgumentNullException("path");
            }

            [Fact]
            public void Should_Set_WorkingDirectory()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.FromPath(@"c:\temp");

                // Then
                settings.WorkingDirectory.ToString().ShouldBe(@"c:/temp");
            }
        }

        public sealed class TheSetMigrationsDll
        {
            [Fact]
            public void Should_Set_MigrationsDll()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetMigrationsDll(@"MyMigrations.dll");

                // Then
                settings.MigrationDll.ShouldBe(@"MyMigrations.dll");
            }
        }

        public sealed class TheSetMigrationsNamespace
        {
            [Fact]
            public void Should_Set_MigrationsNamespace()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetMigrationsNamespace("My.Demo.Migrations");

                // Then
                settings.MigrationsNamespace.ShouldBe("My.Demo.Migrations");
            }
        }

        public sealed class TheSetStartupDll
        {
            [Fact]
            public void Should_Set_StartupDll()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetStartupDll("MyWebUI.dll");

                // Then
                settings.StartupDll.ShouldBe("MyWebUI.dll");
            }
        }

        public sealed class TheSetDbContextClass
        {
            [Fact]
            public void Should_Set_DbContextClass()
            {
                // Given
                var settings = new EfCoreToolSettings();

                // When
                settings.SetDbContextClass("MyDbContext");

                // Then
                settings.DbContextClassName.ShouldBe("MyDbContext");
            }
        }
    }
}
