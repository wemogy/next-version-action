using CommandLine;

namespace Wemogy.ReleaseVersionAction
{
    public class Options
	{
		[Option('r', "repo", Required = true, HelpText = "")]
		public string Repository { get; set; }

		[Option('o', "owner", Required = true, HelpText = "")]
		public string Owner { get; set; }

		[Option('u', "username", Required = true, HelpText = "")]
		public string Username { get; set; }

		[Option('t', "token", Required = true, HelpText = "")]
		public string Token { get; set; }

		[Option('b', "branch", Required = true, HelpText = "")]
		public string Branch { get; set; }
	}
}
