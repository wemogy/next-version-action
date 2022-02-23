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
        public void GetCurrentVersionFromTags_SingleProject_Works()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag { TagName = "0.1.0" },
                new Tag { TagName = "0.1.1" },
            };

            // Act
            var version = VersionHelpers.GetCurrentVersionFromTags(tags, new SemVersion(0, 1, 0), "");

            // Assert
            Assert.Equal(0, version.Major);
            Assert.Equal(1, version.Minor);
            Assert.Equal(1, version.Patch);
        }

        [Fact]
        public void GetCurrentVersionFromTags_MultiProject_Works()
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
            var version = VersionHelpers.GetCurrentVersionFromTags(tags, new SemVersion(0, 2, 0), "configuration");

            // Assert
            Assert.Equal(0, version.Major);
            Assert.Equal(2, version.Minor);
            Assert.Equal(0, version.Patch);
            Assert.Equal("0.2.0", version.ToString());
        }

        [Fact]
        public void IsHighestMinorVersion_SingleProject_Works()
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
            var resultHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 2, 2), string.Empty);
            var resultNonHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 1, 2), string.Empty);

            // Assert
            Assert.True(resultHighest);
            Assert.False(resultNonHighest);
        }

        [Fact]
        public void IsHighestMinorVersion_MultiProject_Works()
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
            var resultHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 2, 2), "helpers");
            var resultNonHighest = VersionHelpers.IsHighestMinorVersion(tags, new SemVersion(0, 1, 2), "helpers");

            // Assert
            Assert.True(resultHighest);
            Assert.False(resultNonHighest);
        }
    }
}
