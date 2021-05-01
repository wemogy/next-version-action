using CommandLine;
using Wemogy.ReleaseVersionAction.Enums;

namespace Wemogy.ReleaseVersionAction
{
    public class Options
	{
		[Option('r', "repo", Required = true, HelpText = "")]
		public string Repository { get; set; }

		[Option('u', "username", Required = true, HelpText = "")]
		public string Username { get; set; }

		[Option('t', "token", Required = true, HelpText = "")]
		public string Token { get; set; }

		[Option('b', "branch", Required = true, HelpText = "")]
		public string Branch { get; set; }

		[Option('p', "project", Default = ProjectType.Single, HelpText = "")]
		public ProjectType ProjectType { get; set; }
	}
}
