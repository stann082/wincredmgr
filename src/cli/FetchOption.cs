using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
using domain;

namespace cli
{
    [Verb("fetch", HelpText = "Fetch credentials from Windows Credential Manager.")]
    public class FetchOption : IFetchOption
    {

        [Option('a', "all", HelpText = "Fetches all secrets (by default only the ones created by user)")]
        public bool All { get; set; }

        [Option('t', "target", HelpText = "Target")]
        public string Target { get; set; }

        [Option('u', "username", HelpText = "Username")]
        public bool Username { get; set; }

        [Option('p', "password", HelpText = "Password")]
        public bool Password { get; set; }

        [Usage(ApplicationAlias = "cli.exe")]
        public static IEnumerable<Example> Examples =>
            new[]
            {
                new Example("Print the username and password for \"github\"",
                    new FetchOption { Target = "github" }),
                new Example("Print only the password, so it can be piped elsewhere",
                    new FetchOption { Target = "github", Password = true }),
                new Example("List every target this tool created",
                    new FetchOption()),
                new Example("List every target in the Windows Credential Manager",
                    new FetchOption { All = true })
            };
    }
}
