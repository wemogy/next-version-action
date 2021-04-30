using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using Semver;
using Wemogy.ReleaseVersionAction.Abstractions;
using Wemogy.ReleaseVersionAction.Helpers;
using Wemogy.ReleaseVersionAction.Services;

namespace Wemogy.ReleaseVersionAction
{
    class Program
    {
		static IGitHubService _gitHubService;

		static  async Task Main(string[] args)
		{
			var result = Parser.Default.ParseArguments<Options>(args);
			await result.MapResult(
				async options => await RunWithOptionsAsync(options),
				errors => HandleParseError(errors)
			);
		}

		static async Task RunWithOptionsAsync(Options options)
		{
			_gitHubService = new GitHubService(options.Username, options.Token);

			Console.WriteLine($"Running in {options.ProjectType} Project mode.");

			var folderName = options.ProjectType == Enums.ProjectType.Multi ? BranchHelpers.ExtractFolderName(options.Branch) : "";
			var currentMajorMinorVersion = BranchHelpers.ExtractMajorMinorVersion(options.Branch, folderName);
			SemVersion nextVersion = currentMajorMinorVersion;
			SemVersion currentVersion = null;

			// Get all tags from GitHub
			Console.Write("GitHub release tags found: ");
			var tags = await _gitHubService.GetAllTagsAsync(options.Owner, options.Repository);
			foreach (var tag in tags)
				Console.Write(tag.TagName + ", ");
			Console.WriteLine();

			// Filter relevant tags only and extract semantic version number only
			var filtered = tags
				.Where(x => x.TagName.Contains(folderName))
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
				currentVersion = sorted.Last();

				// Bump version number
				nextVersion = new SemVersion(currentVersion.Major, currentVersion.Minor, currentVersion.Patch + 1);
			}

			Console.WriteLine(currentVersion != null
				? $"Current version for branch {options.Branch} is: {currentVersion}"
				: $"No current version for branch {options.Branch}");
			Console.WriteLine($"Next version for branch {options.Branch} is: {nextVersion}");
			Console.WriteLine($"::set-output name=next-version::{nextVersion}");
			Console.WriteLine($"::set-output name=folder::{folderName}");
		}

		static Task HandleParseError(IEnumerable<Error> errors)
		{
			Console.WriteLine("Wrong input.");
			return Task.FromResult(1);
		}
    }
}

