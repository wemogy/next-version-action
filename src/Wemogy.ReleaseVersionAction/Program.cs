using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using Wemogy.ReleaseVersionAction.Abstractions;
using Wemogy.ReleaseVersionAction.Services;

namespace Wemogy.ReleaseVersionAction
{
    class Program
    {
		static IGitHubService _gitHubService;

		static void Main(string[] args)
		{
			Parser.Default.ParseArguments<Options>(args)
				.WithParsed(async options => await RunWithOptionsAsync(options))
				.WithNotParsed(HandleParseError);
		}

		static async Task RunWithOptionsAsync(Options options)
		{
			_gitHubService = new GitHubService(options.Username, options.Token);

			// Get all tags from GitHub
			var tags = await _gitHubService.GetAllTagsAsync(options.Owner, options.Repository);

			// Filter relevant tags only and extract semantic version number only
			var filtered = tags
				.Where(x => x.TagName.Contains("")) // TODO: Filter correctly
				.Select(x => x.TagName); // TODO: Extract correctly

			// Check if any tags are found

			// Sort Tags

			// Get latest Tag

			// Bump version number

			var nextVersion = "";
			Console.WriteLine($"::set-output name=next-version::{nextVersion}");
		}

		static void HandleParseError(IEnumerable<Error> errors)
		{
			Console.WriteLine("Wrong input.");
		}
    }
}
