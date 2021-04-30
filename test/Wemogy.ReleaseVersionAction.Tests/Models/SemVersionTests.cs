using Semver;
using Xunit;

namespace Wemogy.ReleaseVersionAction.Tests.Models
{
    public class SemVersionTests
    {
        [Theory]
		[InlineData("1.2.3", 1, 2, 3)]
		[InlineData("1.2", 1, 2, 0)]
        public void Parse_Works(string input, int major, int minor, int patch)
        {
			// Act
			var semver = SemVersion.Parse(input, false);

			// Assert
			Assert.Equal(new SemVersion(major, minor, patch), semver);
        }
    }
}
