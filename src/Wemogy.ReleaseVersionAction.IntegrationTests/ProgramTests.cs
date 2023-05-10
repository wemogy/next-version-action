using System.Threading.Tasks;
using Wemogy.Core.Helpers;
using Wemogy.ReleaseVersionAction.Services;
using Xunit;

namespace Wemogy.ReleaseVersionAction.IntegrationTests
{
    public class ProgramTests
    {
        [Fact]
        public async Task RunAsync_GivenValid_RunsWithoutCrashing()
        {
            // Arrange
            var configuration = ConfigurationFactory.BuildConfiguration();
            var username = configuration["GitHub:Username"];
            var token = configuration["GitHub:Token"];
            var options = new Options
            {
                Token = token,
                Repository = "wemogy/libs-core",
                Username = username,
                Branch = "release/v1.0",
                Projects = Enums.ProjectsType.Single,
                Prefix = "v"
            };

            // Act
            await Program.RunWithOptionsAsync(options);
        }
    }
}
