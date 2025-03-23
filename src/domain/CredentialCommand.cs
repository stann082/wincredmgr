using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CredentialManagement;

namespace domain
{
    public static class CredentialCommand
    {

        #region Constants

        public const string UserSecretsSuffix = "|UserSecrets";

        #endregion
        
        #region Public Methods

        public static async Task<int> Delete(string target)
        {
            using (var credential = new Credential())
            {
                credential.Target = target;
                credential.Delete();
            }

            return await Task.FromResult(0);
        }
        
        public static Task<NetworkCredential> Fetch(string target)
        {
            using (var credential = new Credential())
            {
                credential.Target = GetTargetWithSuffix(target);
                return credential.Load()
                    ? Task.FromResult(new NetworkCredential(credential.Username, credential.Password))
                    : Task.FromResult<NetworkCredential>(null);
            }
        }

        public static Task<string[]> FetchAll()
        {
            using (var credentials = new CredentialSet())
            {
                credentials.Load();
                return Task.FromResult(credentials
                    .Select(c => GetTargetWithSuffix(c.Target))
                    .OrderBy(c => c)
                    .ToArray());
            }
        }
        
        public static async Task<int> Store(IStoreOption opts)
        {
            using (var credential = new Credential())
            {
                credential.Target = GetTargetWithSuffix(opts.Target);
                credential.Username = opts.Username;
                credential.Password = opts.Password;
                credential.Type = CredentialType.Generic;
                credential.PersistanceType = PersistanceType.LocalComputer;
                credential.Save();
            }

            return await Task.FromResult(0);
        }
        
        #endregion

        #region Helper Methods

        private static string GetTargetWithSuffix(string target)
        {
            return $"{target}{UserSecretsSuffix}";
        }

        #endregion

    }
}
