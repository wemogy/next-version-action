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
		public void GetCurrentVersionFromTags_Works()
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
		}
	}
}
