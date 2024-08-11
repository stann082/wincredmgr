using CommandLine;
using domain;

namespace cli
{
    [Verb("store", HelpText = "Store credentials in Windows Credential Manager.")]
    public class StoreOption : IStoreOption
    {
        [Option('t', "target", Required = true, HelpText = "Target")]
        public string Target { get; set; }
        
        [Option('u', "username", Required = true, HelpText = "Username")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password")]
        public string Password { get; set; }
    }
}
