using Wemogy.ReleaseVersionAction.Models;

namespace Wemogy.ReleaseVersionAction.Helpers
{
    public static class TagHelpers
    {
        public static string ExtractVersion(Tag tag, string folderName, string prefix)
        {
            var version = string.IsNullOrEmpty(folderName)
                ? tag.TagName
                : tag.TagName.Replace($"{folderName}-", string.Empty);

            if (!string.IsNullOrEmpty(prefix))
            {
                version = version.Replace(prefix, string.Empty);
            }

            return version;
        }
    }
}
