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
            return await Parser.Default.ParseArguments<StoreOption, FetchOption>(args)
                .MapResult(
                    async (StoreOption opts) => await Store(opts),
                    async (FetchOption opts) => await Fetch(opts),
                    _ => Task.FromResult(1));
        }

        #endregion

        #region Helper Methods

        private static async Task<int> Delete(IOption opts)
        {
            return await new CredentialCommand().Delete(opts.Target);
        }

        private static async Task<int> Exists(IOption opts)
        {
            return await new CredentialCommand().Exists(opts.Target) ? 0 : 1;
        }

        private static async Task<int> Fetch(IOption opts)
        {
            var credentials = await new CredentialCommand().Fetch(opts.Target);
            Console.WriteLine($"{credentials.UserName} {credentials.Password}");
            return await Task.FromResult(0);
        }

        private static async Task<int> Store(IStoreOption opts)
        {
            return await new CredentialCommand().Store(opts);
        }

        #endregion

    }
}
