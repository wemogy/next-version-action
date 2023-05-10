using System.Threading.Tasks;
using Wemogy.Configuration;
using Wemogy.Core.Helpers;
using Wemogy.ReleaseVersionAction.Services;
using Xunit;

namespace Wemogy.ReleaseVersionAction.IntegrationTests.Services
{
    public class GitHubServiceTests
    {
        private static GitHubService Setup()
        {
            var configuration = ConfigurationFactory.BuildConfiguration("Development");
            var username = configuration["GitHub:Username"];
            var token = configuration["GitHub:Token"];

            Skip.If(string.IsNullOrEmpty(username), "No GitHub Username was given.");
            Skip.If(string.IsNullOrEmpty(token), "No GitHub Token was given.");

            return new GitHubService(username, token);
        }

        [SkippableFact]
        public async Task GetAllTagsAsync_ReturnsTags()
        {
            // Arrange
            var gitHubService = Setup();

            // Act
            var tags = await gitHubService.GetAllTagsAsync("wemogy/libs");

            // Assert
            Assert.NotEmpty(tags);
        }
    }
}
