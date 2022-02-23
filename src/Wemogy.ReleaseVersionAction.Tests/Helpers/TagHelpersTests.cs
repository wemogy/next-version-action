using Wemogy.ReleaseVersionAction.Helpers;
using Wemogy.ReleaseVersionAction.Models;
using Xunit;

namespace Wemogy.ReleaseVersionAction.Tests.Helpers
{
    public class TagHelpersTests
    {
        [Theory]
        [InlineData("configuration-v1.2.3", "configuration", "1.2.3")]
        [InlineData("data-access-v0.1.4", "data-access", "0.1.4")]
        [InlineData("v0.1.4", "", "0.1.4")]
        public void ExtractVersion_GivenPrefix_Works(string tagName, string folderName, string expected)
        {
            // Arrange
            var tag = new Tag { TagName = tagName };

            // Act
            var version = TagHelpers.ExtractVersion(tag, folderName, "v");

            // Assert
            Assert.Equal(expected, version);
        }

        [Theory]
        [InlineData("configuration-1.2.3", "configuration", "1.2.3")]
        [InlineData("data-access-0.1.4", "data-access", "0.1.4")]
        [InlineData("verbose-0.1.4", "verbose", "0.1.4")]
        [InlineData("0.1.4", "", "0.1.4")]
        public void ExtractVersion_GivenNoPrefix_Works(string tagName, string folderName, string expected)
        {
            // Arrange
            var tag = new Tag { TagName = tagName };

            // Act
            var version = TagHelpers.ExtractVersion(tag, folderName, string.Empty);

            // Assert
            Assert.Equal(expected, version);
        }
    }
}
