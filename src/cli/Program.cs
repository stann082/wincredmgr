using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using domain;

namespace cli
{
    internal static class Program
    {

        #region Main Entry Point

        public static async Task<int> Main(string[] args)
        {
            if (UsageText.IsTopLevelHelpRequest(args))
            {
                Console.WriteLine(UsageText.Render());
                return 0;
            }

            // The parser only understands "--help"; HelpWriter is disabled so that requested help
            // goes to stdout and genuine parse errors go to stderr.
            var parser = new Parser(with => with.HelpWriter = null);
            var result = parser.ParseArguments<StoreOption, FetchOption, DeleteOption>(
                UsageText.NormalizeVerbHelpFlag(args));

            return await result.MapResult(
                async (StoreOption opts) => await Store(opts),
                async (FetchOption opts) => await Fetch(opts),
                async (DeleteOption opts) => await Delete(opts),
                errs => Task.FromResult(HandleParseErrors(result, errs)));
        }

        #endregion

        #region Helper Methods

        private static async Task<int> Delete(IOption opts)
        {
            return await CredentialCommand.Delete(opts.Target);
        }

        private static async Task<int> Fetch(IFetchOption opts)
        {
            if (string.IsNullOrWhiteSpace(opts.Target))
            {
                var targets = await CredentialCommand.FetchAll(opts.All);
                foreach (var target in targets)
                {
                    Console.WriteLine(target.Replace($"{CredentialCommand.UserSecretsSuffix}", string.Empty));
                }

                return 0;
            }

            var credentials = await CredentialCommand.Fetch(opts.Target);
            if (credentials == null)
            {
                Console.WriteLine($"Target {opts.Target} does not exist");
                return 1;
            }

            if (opts.Password)
            {
                Console.WriteLine($"{credentials.Password}");
                return 0;
            }

            if (opts.Username)
            {
                Console.WriteLine($"{credentials.UserName}");
                return 0;
            }

            Console.WriteLine($"{credentials.UserName} {credentials.Password}");
            return await Task.FromResult(0);
        }

        private static int HandleParseErrors(ParserResult<object> result, IEnumerable<Error> errors)
        {
            var errorList = errors.ToList();
            var helpRequested = errorList.Any(e => e.Tag == ErrorType.HelpRequestedError
                                                   || e.Tag == ErrorType.HelpVerbRequestedError
                                                   || e.Tag == ErrorType.VersionRequestedError);

            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);

            if (helpRequested)
            {
                Console.WriteLine(helpText);
                return 0;
            }

            Console.Error.WriteLine(helpText);
            return 1;
        }

        private static async Task<int> Store(IStoreOption opts)
        {
            return await CredentialCommand.Store(opts);
        }

        #endregion

    }
}
