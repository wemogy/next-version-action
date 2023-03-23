using System.Collections.Generic;
using Semver;
using Wemogy.ReleaseVersionAction.Helpers;
using Wemogy.ReleaseVersionAction.Models;
using Xunit;

namespace Wemogy.ReleaseVersionAction.Tests.Helpers
{
    public class VersionHelpersTests
    {
        [Fact]
        public void GetCurrentVersionFromTags_SingleProject_GivenNoPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "0.1.0" },
                new Tag { TagName = "0.1.1" },
            };

            // Act
            var version = VersionHelpers.GetCurrentVersionFromTags(tags, new SemVersion(0, 1, 0), string.Empty, string.Empty);

            // Assert
            Assert.Equal(0, version.Major);
            Assert.Equal(1, version.Minor);
            Assert.Equal(1, version.Patch);
        }

        [Fact]
        public void GetCurrentVersionFromTags_SingleProject_GivenNonExisting_ReturnsNull()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "0.1.0" },
                new Tag { TagName = "0.1.1" },
            };

            // Act
            var version = VersionHelpers.GetCurrentVersionFromTags(tags, new SemVersion(0, 2, 0), string.Empty, string.Empty);

            // Assert
            Assert.Null(version);
        }

        [Fact]
        public void GetCurrentVersionFromTags_SingleProject_GivenPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "v0.1.0" },
                new Tag { TagName = "v0.1.1" },
            };

            // Act
            var version = VersionHelpers.GetCurrentVersionFromTags(tags, new SemVersion(0, 1, 0), string.Empty, "v");

            // Assert
            Assert.Equal(0, version.Major);
            Assert.Equal(1, version.Minor);
            Assert.Equal(1, version.Patch);
        }

        [Fact]
        public void GetCurrentVersionFromTags_MultiProject_GivenNoPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "helpers-0.1.52" },
                new Tag { TagName = "configuration-0.1.0" },
                new Tag { TagName = "configuration-0.1.1" },
                new Tag { TagName = "service-configuration-0.1.0" },
                new Tag { TagName = "configuration-0.2.0" }
            };

            // Act
            var version = VersionHelpers.GetCurrentVersionFromTags(tags, new SemVersion(0, 2, 0), "configuration", string.Empty);

            // Assert
            Assert.Equal(0, version.Major);
            Assert.Equal(2, version.Minor);
            Assert.Equal(0, version.Patch);
            Assert.Equal("0.2.0", version.ToString());
        }

        [Fact]
        public void GetCurrentVersionFromTags_MultiProject_GivenPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "helpers-v0.1.52" },
                new Tag { TagName = "configuration-v0.1.0" },
                new Tag { TagName = "configuration-v0.1.1" },
                new Tag { TagName = "service-configuration-v0.1.0" },
                new Tag { TagName = "configuration-v0.2.0" }
            };

            // Act
            var version = VersionHelpers.GetCurrentVersionFromTags(tags, new SemVersion(0, 2, 0), "configuration", "v");

            // Assert
            Assert.Equal(0, version.Major);
            Assert.Equal(2, version.Minor);
            Assert.Equal(0, version.Patch);
            Assert.Equal("0.2.0", version.ToString());
        }

        [Fact]
        public void IsHighestMinorVersion_SingleProject_GivenNoPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "0.1.0" },
                new Tag { TagName = "0.1.1" },
                new Tag { TagName = "0.1.2" },
                new Tag { TagName = "0.2.1" },
                new Tag { TagName = "0.2.2" },
                new Tag { TagName = "1.2.3" },
            };

            // Act
            var resultHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 2, 2), string.Empty, string.Empty);
            var resultNonHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 1, 2), string.Empty, string.Empty);
            var resultNewMajor = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(2, 0, 0), string.Empty, string.Empty);

            // Assert
            Assert.True(resultHighest);
            Assert.False(resultNonHighest);
            Assert.True(resultNewMajor);
        }

        [Fact]
        public void IsHighestMinorVersion_SingleProject_GivenPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "v0.1.0" },
                new Tag { TagName = "v0.1.1" },
                new Tag { TagName = "v0.1.2" },
                new Tag { TagName = "v0.2.1" },
                new Tag { TagName = "v0.2.2" },
                new Tag { TagName = "v1.2.3" },
            };

            // Act
            var resultHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 2, 2), string.Empty, "v");
            var resultNonHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 1, 2), string.Empty, "v");
            var resultNewMajor = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(2, 0, 0), string.Empty, "v");

            // Assert
            Assert.True(resultHighest);
            Assert.False(resultNonHighest);
            Assert.True(resultNewMajor);
        }

        [Fact]
        public void IsHighestMinorVersion_MultiProject_GivenNoPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "helpers-0.1.0" },
                new Tag { TagName = "helpers-0.1.1" },
                new Tag { TagName = "helpers-0.1.2" },
                new Tag { TagName = "helpers-0.2.1" },
                new Tag { TagName = "helpers-0.2.2" },
                new Tag { TagName = "helpers-1.2.3" },
                new Tag { TagName = "configuration-0.1.0" },
                new Tag { TagName = "configuration-0.1.1" },
                new Tag { TagName = "configuration-0.1.2" },
                new Tag { TagName = "configuration-0.2.1" },
                new Tag { TagName = "configuration-0.2.2" },
                new Tag { TagName = "configuration-0.2.3" },
                new Tag { TagName = "configuration-1.2.3" }
            };

            // Act
            var resultHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 2, 2), "helpers", string.Empty);
            var resultNonHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 1, 2), "helpers", string.Empty);
            var resultNewMajor = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(2, 0, 0), "helpers", string.Empty);

            // Assert
            Assert.True(resultHighest);
            Assert.False(resultNonHighest);
            Assert.True(resultNewMajor);
        }

        [Fact]
        public void IsHighestMinorVersion_MultiProject_GivenPrefix_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "helpers-v0.1.0" },
                new Tag { TagName = "helpers-v0.1.1" },
                new Tag { TagName = "helpers-v0.1.2" },
                new Tag { TagName = "helpers-v0.2.1" },
                new Tag { TagName = "helpers-v0.2.2" },
                new Tag { TagName = "helpers-v1.2.3" },
                new Tag { TagName = "configuration-v0.1.0" },
                new Tag { TagName = "configuration-v0.1.1" },
                new Tag { TagName = "configuration-v0.1.2" },
                new Tag { TagName = "configuration-v0.2.1" },
                new Tag { TagName = "configuration-v0.2.2" },
                new Tag { TagName = "configuration-v0.2.3" },
                new Tag { TagName = "configuration-v1.2.3" }
            };

            // Act
            var resultHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 2, 2), "helpers", "v");
            var resultNonHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 1, 2), "helpers", "v");
            var resultNewMajor = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(2, 0, 0), "helpers", "v");

            // Assert
            Assert.True(resultHighest);
            Assert.False(resultNonHighest);
            Assert.True(resultNewMajor);
        }

        [Fact]
        public void IsHighestMinorVersion_GivenNoTags_ReturnsTrue()
        {
            // Arrange
            var tags = new List<Tag>();

            // Act
            var resultHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 2, 2), string.Empty, "v");

            // Assert
            Assert.True(resultHighest);
        }
    }
}
