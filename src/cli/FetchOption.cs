using CommandLine;
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
    }
}
