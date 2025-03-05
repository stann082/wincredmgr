using System.Threading.Tasks;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class CredentialCommandTest
    {

        private const string MockEntry = "mockEntry";
        
        [SetUp]
        public async Task SetUp()
        {
            await CredentialCommand.Delete(MockEntry);
        }
        
        [Test]
        public async Task TestBlueSky_StoreAndFetchCredentials()
        {
            // setup
            var credential = await CredentialCommand.Fetch(MockEntry);
            
            // pre-conditions
            Assert.That(credential, Is.Null);
            
            // exercise
            await CredentialCommand.Store(new MockStoreOption
            {
                Target = MockEntry,
                Username = "mockEntryUsername",
                Password = "mockEntryPassword"
            });
            
            // post-conditions
            credential = await CredentialCommand.Fetch(MockEntry);
            Assert.That(credential, Is.Not.Null);
            Assert.That(credential.UserName, Is.EqualTo("mockEntryUsername"));
            Assert.That(credential.Password, Is.EqualTo("mockEntryPassword"));
        }

        [TearDown]
        public async Task TearDown()
        {
            await CredentialCommand.Delete(MockEntry);
        }
        
        #region Mock Data

        private class MockStoreOption : IStoreOption
        {
            public string Target { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        #endregion
        
    }
}
