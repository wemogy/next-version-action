using Wemogy.ReleaseVersionAction.Helpers;
using Xunit;

namespace Wemogy.ReleaseVersionAction.Tests.Helpers
{
    public class BranchHelpersTests
    {
        [Theory]
        [InlineData("refs/heads/release/configuration/0.1", "configuration")]
        [InlineData("refs/heads/release/data-access/0.2", "data-access")]
        [InlineData("release/data-access/0.2", "data-access")]
        public void ExtractFolderName_Works(string branch, string expected)
        {
            // Act
            var folderName = BranchHelpers.ExtractFolderName(branch);

            // Assert
            Assert.Equal(expected, folderName);
        }

        [Theory]
        [InlineData("refs/heads/release/configuration/0.1", "configuration", 0, 1)]
        [InlineData("refs/heads/release/data-access/0.2", "data-access", 0, 2)]
        [InlineData("release/data-access/0.2", "data-access", 0, 2)]
        [InlineData("refs/heads/release/0.2", "", 0, 2)]
        [InlineData("release/0.2", "", 0, 2)]
        public void ExtractMajorMinorVersion_Works(string branch, string folderName, int expectedMajor, int expectedMinor)
        {
            // Act
            var version = BranchHelpers.ExtractMajorMinorVersion(branch, folderName);

            // Assert
            Assert.Equal(expectedMajor, version.Major);
            Assert.Equal(expectedMinor, version.Minor);
            Assert.Equal(0, version.Patch);
        }
    }
}
