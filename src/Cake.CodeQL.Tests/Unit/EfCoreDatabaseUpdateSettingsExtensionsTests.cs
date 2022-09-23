using Cake.EntityFrameworkCore.Database;
using Shouldly;
using Xunit;

namespace Cake.EntityFrameworkCore.Tests
{
    public sealed class EfCoreDatabaseUpdateSettingsExtensionsTests
    {
        public sealed class TheSetTargetMigration
        {
            [Fact]
            public void Should_Set_Migrations()
            {
                // Given
                var settings = new EfCoreDatabaseUpdateSettings();

                // When
                settings.SetTargetMigration(@"20201221_MyTable_Change");

                // Then
                settings.Migration.ShouldBe(@"20201221_MyTable_Change");
            }
        }
    }
}
