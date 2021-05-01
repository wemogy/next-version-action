using Semver;

namespace Wemogy.ReleaseVersionAction.Helpers
{
    public static class BranchHelpers
    {
        public static string ExtractFolderName(string branch)
		{
			branch = branch.Replace("refs/heads/", "");
			branch = branch.Replace("release/", "");
			return branch.Substring(0, branch.LastIndexOf("/"));
		}

		public static SemVersion ExtractMajorMinorVersion(string branch, string folderName)
		{
			string version;
			branch = branch.Replace("refs/heads/", "");

			if (string.IsNullOrEmpty(folderName))
				version = branch.Replace($"release/", "");
			else
				version = branch.Replace($"release/{folderName}/", "");
			return SemVersion.Parse(version);
		}
    }
}
