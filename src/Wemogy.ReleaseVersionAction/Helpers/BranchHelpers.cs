using Semver;

namespace Wemogy.ReleaseVersionAction.Helpers
{
    public static class BranchHelpers
    {
        public static string ExtractFolderName(string branch)
		{
			var shortened = branch.Replace("refs/heads/release/", "");
			return shortened.Substring(0, shortened.LastIndexOf("/"));
		}

		public static SemVersion ExtractMajorMinorVersion(string branch, string folderName)
		{
			string version;

			if (string.IsNullOrEmpty(folderName))
				version = branch.Replace($"refs/heads/release/", "");
			else
				version = branch.Replace($"refs/heads/release/{folderName}/", "");
			return SemVersion.Parse(version);
		}
    }
}
