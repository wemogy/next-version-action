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

		static async Task Main(string[] args)
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

			Console.WriteLine($"Running in {options.Projects} Project mode.");

			var folderName = options.Projects == Enums.ProjectsType.Multi
				? BranchHelpers.ExtractFolderName(options.Branch)
				: "";

			// Get all tags from GitHub
			Console.Write("GitHub release tags found: ");
			var tags = await _gitHubService.GetAllTagsAsync(options.Repository);
			foreach (var tag in tags)
				Console.Write(tag.TagName + ", ");
			Console.WriteLine();

			// Get current version
			var currentMajorMinorVersion = BranchHelpers.ExtractMajorMinorVersion(options.Branch, folderName);
			SemVersion nextVersion = currentMajorMinorVersion;
			SemVersion currentVersion = VersionHelpers.GetCurrentVersionFromTags(tags, currentMajorMinorVersion, folderName);
			if (currentVersion != null)
			{
				// Update next version
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
			Environment.Exit(1);
			return Task.CompletedTask;
		}
	}
}
