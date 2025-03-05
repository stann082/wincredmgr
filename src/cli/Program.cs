using System;
using System.Threading.Tasks;
using CommandLine;
using domain;

namespace cli
{
    internal static class Program
    {

        #region Main Entry Point

        public static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<StoreOption, FetchOption, DeleteOption>(args)
                .MapResult(
                    async (StoreOption opts) => await Store(opts),
                    async (FetchOption opts) => await Fetch(opts),
                    async (DeleteOption opts) => await Delete(opts),
                    _ => Task.FromResult(1));
        }

        #endregion

        #region Helper Methods

        private static async Task<int> Delete(IOption opts)
        {
            return await CredentialCommand.Delete(opts.Target);
        }

        private static async Task<int> Fetch(IOption opts)
        {
            var credentials = await CredentialCommand.Fetch(opts.Target);
            if (credentials == null)
            {
                Console.WriteLine($"Target {opts.Target} does not exist");
                return 1;
            }

            Console.WriteLine($"{credentials.UserName} {credentials.Password}");
            return await Task.FromResult(0);
        }

        private static async Task<int> Store(IStoreOption opts)
        {
            return await CredentialCommand.Store(opts);
        }

        #endregion

    }
}
