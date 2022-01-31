using System.Collections.Generic;
using System.Linq;
using Semver;
using Wemogy.ReleaseVersionAction.Models;

namespace Wemogy.ReleaseVersionAction.Helpers
{
	public static class VersionHelpers
	{
		public static SemVersion GetCurrentVersionFromTags(List<Tag> tags, SemVersion currentMajorMinorVersion, string folderName)
		{
			// Filter relevant tags only and extract semantic version number only
			var filtered = tags
				.Where(x => x.TagName.Substring(0, x.TagName.LastIndexOf("-")).Equals(folderName))
				.Select(x => SemVersion.Parse(TagHelpers.ExtractVersion(x, folderName)))
				.Where(x => x.Major == currentMajorMinorVersion.Major && x.Minor == currentMajorMinorVersion.Minor)
				.ToList();

			if (filtered.Any())
			{
				// Sort Tags lowest first
				var sorted = filtered
					.OrderBy(x => x.Major)
					.ThenBy(x => x.Minor)
					.ThenBy(x => x.Patch)
					.ThenBy(x => x.Prerelease);

				// Get latest Tag
				return sorted.Last();
			}

			return currentMajorMinorVersion;
		}
	}
}
