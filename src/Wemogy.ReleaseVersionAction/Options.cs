using CommandLine;
using Wemogy.ReleaseVersionAction.Enums;

namespace Wemogy.ReleaseVersionAction
{
    public class Options
    {
        [Option('t', "token", Required = true, HelpText = "A GitHub Access Token")]
        public string Token { get; set; }

        [Option('r', "repo", Required = true, HelpText = "The repository (owner/name)")]
        public string Repository { get; set; }

        [Option('u', "username", Required = true, HelpText = "The GitHub username")]
        public string Username { get; set; }

        [Option('b', "branch", Required = true, HelpText = "The release branch to check")]
        public string Branch { get; set; }

        [Option('p', "projects", Default = ProjectsType.Single, HelpText = "The amount of projects in this repo (Single or Multi)")]
        public ProjectsType Projects { get; set; }

        [Option('x', "prefix", Default = "", HelpText = "A prefix to all versions")]
        public string Prefix { get; set; }
    }
}
