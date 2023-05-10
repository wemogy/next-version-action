using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommandLine;
using Semver;
using Wemogy.ReleaseVersionAction.Abstractions;
using Wemogy.ReleaseVersionAction.Helpers;
using Wemogy.ReleaseVersionAction.Services;

namespace Wemogy.ReleaseVersionAction
{
    public class Program
    {
        static IGitHubService _gitHubService;

        static async Task Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            await result.MapResult(
                async options => await RunWithOptionsAsync(options),
                errors => HandleParseError(errors));
        }

        public static async Task RunWithOptionsAsync(Options options)
        {
            _gitHubService = new GitHubService(options.Username, options.Token);

            Console.WriteLine($"Running in {options.Projects} Project mode.");

            var folderName = options.Projects == Enums.ProjectsType.Multi
                ? BranchHelpers.ExtractFolderName(options.Branch)
                : string.Empty;

            // Get all tags from GitHub
            Console.Write("GitHub release tags found: ");
            var allTags = await _gitHubService.GetAllTagsAsync(options.Repository);
            foreach (var tag in allTags)
            {
                Console.Write(tag.TagName + ", ");
            }

            Console.WriteLine();

            // Get the major + minor version for the branch
            // Example: 1.2.0 for the branch release/1.2
            var branchMajorMinorVersion = BranchHelpers.ExtractMajorMinorVersion(options.Branch, folderName, options.Prefix);

            // Get the latest released version for this branch by tags
            // Example: v1.2.8 for the branch release/1.2
            var branchCurrentVersion = VersionHelpers.GetCurrentVersionFromTags(allTags, branchMajorMinorVersion, folderName, options.Prefix);

            // Get version for the next release
            SemVersion nextVersion;
            if (branchCurrentVersion == null)
            {
                // No current version for this branch available so this is the first release for this branch.
                // This means the next version is the major.minor.0 version
                nextVersion = branchMajorMinorVersion;
            }
            else
            {
                // Increase the patch version by one
                nextVersion = new SemVersion(branchCurrentVersion.Major, branchCurrentVersion.Minor, branchCurrentVersion.Patch + 1);
            }

            // Set tags for the current major.minor and major.minor.patch version
            // Example: v1.2 and v1.2.4
            var tagsToSet = new List<string>
            {
                $"{options.Prefix}{branchMajorMinorVersion.Major}.{branchMajorMinorVersion.Minor}",
                $"{options.Prefix}{nextVersion}",
            };

            // If the next version is the highest one amongst other tags with the same major version, set its tag
            // Example: v1
            var isHightestVersion = VersionHelpers.IsHighestMinorVersion(allTags, nextVersion, folderName, options.Prefix);
            if (isHightestVersion)
            {
                tagsToSet.Add($"{options.Prefix}{branchMajorMinorVersion.Major}");
            }

            Console.WriteLine(branchCurrentVersion != null
                ? $"Current version for branch {options.Branch} is: {branchCurrentVersion}"
                : $"No current version for branch {options.Branch}");
            Console.WriteLine($"Next version for branch {options.Branch} is: {nextVersion}");
            Console.WriteLine($"::set-output name=next-version::{nextVersion}");
            Console.WriteLine($"::set-output name=next-version-name::{options.Prefix}{nextVersion}");
            Console.WriteLine($"::set-output name=folder::{folderName}");
            Console.WriteLine($"::set-output name=tags::{JsonSerializer.Serialize(tagsToSet)}");
        }

        static Task HandleParseError(IEnumerable<Error> errors)
        {
            Environment.Exit(1);
            return Task.CompletedTask;
        }
    }
}
