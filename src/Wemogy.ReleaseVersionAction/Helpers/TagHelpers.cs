using Wemogy.ReleaseVersionAction.Models;

namespace Wemogy.ReleaseVersionAction.Helpers
{
    public static class TagHelpers
    {
        public static string ExtractVersion(Tag tag, string folderName)
		{
			return string.IsNullOrEmpty(folderName) ? tag.TagName : tag.TagName.Replace($"{folderName}-", "");
		}
    }
}
