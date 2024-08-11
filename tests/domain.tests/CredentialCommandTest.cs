using System.Threading.Tasks;
using NUnit.Framework;

namespace domain.tests
{
    [TestFixture]
    public class CredentialCommandTest
    {

        private const string MOCK_ENTRY = "mockEntry";
        private CredentialCommand _command;
        
        [SetUp]
        public async Task SetUp()
        {
            _command = new CredentialCommand();
            await _command.Delete(MOCK_ENTRY);
        }
        
        [Test]
        public async Task TestBlueSky_StoreAndFetchCredentials()
        {
            // setup
            var credential = await _command.Fetch(MOCK_ENTRY);
            
            // pre-conditions
            Assert.That(credential, Is.Null);
            
            // exercise
            await _command.Store(new MockStoreOption
            {
                Target = MOCK_ENTRY,
                Username = "mockEntryUsername",
                Password = "mockEntryPassword"
            });
            
            // post-conditions
            credential = await _command.Fetch(MOCK_ENTRY);
            Assert.That(credential, Is.Not.Null);
            Assert.That(credential.UserName, Is.EqualTo("mockEntryUsername"));
            Assert.That(credential.Password, Is.EqualTo("mockEntryPassword"));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _command.Delete(MOCK_ENTRY);
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
