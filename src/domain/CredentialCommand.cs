using System.Net;
using System.Threading.Tasks;
using CredentialManagement;

namespace domain
{
    public class CredentialCommand
    {
        
        #region Public Methods

        public async Task<int> Delete(string target)
        {
            using (var credential = new Credential())
            {
                credential.Target = target;
                credential.Delete();
            }

            return await Task.FromResult(0);
        }
        
        public Task<NetworkCredential> Fetch(string target)
        {
            using (var credential = new Credential())
            {
                credential.Target = target;
                return !credential.Load()
                    ? Task.FromResult<NetworkCredential>(null)
                    : Task.FromResult(new NetworkCredential(credential.Username, credential.Password));
            }
        }
        
        public async Task<int> Store(IStoreOption opts)
        {
            using (var credential = new Credential())
            {
                credential.Target = opts.Target;
                credential.Username = opts.Username;
                credential.Password = opts.Password;
                credential.Type = CredentialType.Generic;
                credential.PersistanceType = PersistanceType.LocalComputer;
                credential.Save();
            }

            return await Task.FromResult(0);
        }
        
        #endregion

    }
}
