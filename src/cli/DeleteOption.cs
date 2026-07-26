using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
using domain;

namespace cli
{
    [Verb("delete", HelpText = "Delete credentials from Windows Credential Manager.")]
    public class DeleteOption : IOption
    {

        [Option('t', "target", Required = true, HelpText = "Target")]
        public string Target { get; set; }

        [Usage(ApplicationAlias = "cli.exe")]
        public static IEnumerable<Example> Examples =>
            new[]
            {
                new Example("Delete the \"github\" credential", new DeleteOption { Target = "github" })
            };
    }
}
