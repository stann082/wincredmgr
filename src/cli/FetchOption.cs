using CommandLine;
using domain;

namespace cli
{
    [Verb("fetch", HelpText = "Fetch credentials from Windows Credential Manager.")]
    public class FetchOption : IOption
    {
        
        [Option('t', "target", Required = true, HelpText = "Target")]
        public string Target { get; set; }
        
    }
}
