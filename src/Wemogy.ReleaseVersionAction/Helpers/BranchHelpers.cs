using Semver;

namespace Wemogy.ReleaseVersionAction.Helpers
{
    public static class BranchHelpers
    {
        public static string ExtractFolderName(string branch)
        {
            branch = branch.Replace("refs/heads/", string.Empty);
            branch = branch.Replace("release/", string.Empty);
            return branch.Substring(0, branch.LastIndexOf("/"));
        }

        public static SemVersion ExtractMajorMinorVersion(string branch, string folderName)
        {
            string version;
            branch = branch.Replace("refs/heads/", string.Empty);

            if (string.IsNullOrEmpty(folderName))
            {
                version = branch.Replace($"release/", string.Empty);
            }
            else
            {
                version = branch.Replace($"release/{folderName}/", string.Empty);
            }

            return SemVersion.Parse(version);
        }
    }
}
