using Wemogy.ReleaseVersionAction.Helpers;
using Wemogy.ReleaseVersionAction.Models;
using Xunit;

namespace Wemogy.ReleaseVersionAction.Tests.Helpers
{
    public class TagHelpersTests
    {
        [Theory]
		[InlineData("configuration-1.2.3", "configuration", "1.2.3")]
		[InlineData("data-access-0.1.4", "data-access", "0.1.4")]
		[InlineData("0.1.4", "", "0.1.4")]
		public void ExtractVersion_Works(string tagName, string folderName, string expected)
		{
			// Arrange
			var tag = new Tag { TagName = tagName };

			// Act
			var version = TagHelpers.ExtractVersion(tag, folderName);

			// Assert
			Assert.Equal(expected, version);
		}
    }
}
