using CommandLine;
using domain;

namespace cli
{
    [Verb("delete", HelpText = "Delete credentials from Windows Credential Manager.")]
    public class DeleteOption : IOption
    {
        
        [Option('t', "target", Required = true, HelpText = "Target")]
        public string Target { get; set; }
        
    }
}
