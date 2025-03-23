using CommandLine;
using domain;

namespace cli
{
    public class Options : IOptions
    {
        
        [Option('p', "password", HelpText = "Password")]
        public string Password { get; set; }
        
        [Option('t', "target", Required = true, HelpText = "Target")]
        public string Target { get; set; }
        
        [Option('u', "username", HelpText = "Username")]
        public string Username { get; set; }

    }
}