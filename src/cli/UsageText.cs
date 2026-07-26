using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace cli
{
    internal static class UsageText
    {

        #region Public Methods

        public static bool IsTopLevelHelpRequest(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return true;
            }

            return args.Length == 1 && IsHelpToken(args[0]);
        }

        /// <summary>
        /// Rewrites a help flag in the verb position so that "&lt;verb&gt; -h" and "&lt;verb&gt; /?" behave
        /// like "&lt;verb&gt; --help", which is the only form the parser recognizes on its own.
        /// </summary>
        public static string[] NormalizeVerbHelpFlag(string[] args)
        {
            if (args == null || args.Length < 2 || !IsHelpFlag(args[1]))
            {
                return args;
            }

            var normalized = (string[])args.Clone();
            normalized[1] = "--help";
            return normalized;
        }

        public static string Render()
        {
            var exe = ExecutableName;
            var builder = new StringBuilder();

            builder.AppendLine($"{exe} {Version}");
            builder.AppendLine("Store, fetch, and delete generic credentials in the Windows Credential Manager.");
            builder.AppendLine("Targets are saved with a \"|UserSecrets\" suffix so they can be told apart from");
            builder.AppendLine("credentials created by other applications.");
            builder.AppendLine();

            builder.AppendLine("USAGE");
            builder.AppendLine($"  {exe} <command> [options]");
            builder.AppendLine();

            builder.AppendLine("COMMANDS");
            builder.AppendLine("  store     Store credentials in Windows Credential Manager.");
            builder.AppendLine("  fetch     Fetch credentials from Windows Credential Manager.");
            builder.AppendLine("  delete    Delete credentials from Windows Credential Manager.");
            builder.AppendLine();

            builder.AppendLine("OPTIONS");
            builder.AppendLine("  store");
            builder.AppendLine("    -t, --target      Required. Name of the credential to store.");
            builder.AppendLine("    -u, --username    Required. Username to store.");
            builder.AppendLine("    -p, --password    Required. Password to store.");
            builder.AppendLine();
            builder.AppendLine("  fetch");
            builder.AppendLine("    -t, --target      Name of the credential to fetch. Omit it to list targets.");
            builder.AppendLine("    -u, --username    Print only the username.");
            builder.AppendLine("    -p, --password    Print only the password.");
            builder.AppendLine("    -a, --all         When listing, include credentials this tool did not create.");
            builder.AppendLine();
            builder.AppendLine("  delete");
            builder.AppendLine("    -t, --target      Required. Name of the credential to delete.");
            builder.AppendLine();
            builder.AppendLine("  -h, --help          Show this help text.");
            builder.AppendLine($"                      Use \"{exe} <command> --help\" for a single command.");
            builder.AppendLine("  --version           Show version information.");
            builder.AppendLine();

            builder.AppendLine("EXAMPLES");
            builder.AppendLine("  Store a credential named \"github\":");
            builder.AppendLine($"    {exe} store --target github --username octocat --password s3cret");
            builder.AppendLine();
            builder.AppendLine("  Print the username and password for \"github\":");
            builder.AppendLine($"    {exe} fetch --target github");
            builder.AppendLine();
            builder.AppendLine("  Print only the password, so it can be piped into another command:");
            builder.AppendLine($"    {exe} fetch --target github --password");
            builder.AppendLine();
            builder.AppendLine("  List every target this tool created:");
            builder.AppendLine($"    {exe} fetch");
            builder.AppendLine();
            builder.AppendLine("  List every target in the Windows Credential Manager:");
            builder.AppendLine($"    {exe} fetch --all");
            builder.AppendLine();
            builder.AppendLine("  Delete the \"github\" credential:");
            builder.AppendLine($"    {exe} delete --target github");
            builder.AppendLine();

            builder.AppendLine("EXIT CODES");
            builder.AppendLine("  0    Success.");
            builder.AppendLine("  1    The target was not found, or the arguments were invalid.");

            return builder.ToString();
        }

        #endregion

        #region Properties

        public static string ExecutableName
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                var name = Path.GetFileName(assembly.Location);
                return string.IsNullOrWhiteSpace(name) ? "cli.exe" : name;
            }
        }

        #endregion

        #region Helper Methods

        private static bool IsHelpFlag(string arg)
        {
            return string.Equals(arg, "-h", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(arg, "--help", StringComparison.OrdinalIgnoreCase)
                   || arg == "-?"
                   || arg == "/?";
        }

        private static bool IsHelpToken(string arg)
        {
            return IsHelpFlag(arg) || string.Equals(arg, "help", StringComparison.OrdinalIgnoreCase);
        }

        private static string Version
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                return assembly.GetName().Version.ToString();
            }
        }

        #endregion

    }
}
