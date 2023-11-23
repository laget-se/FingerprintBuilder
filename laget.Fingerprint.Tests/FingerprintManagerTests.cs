using laget.Fingerprint.Stores;
using System;
using System.Threading.Tasks;
using Xunit;

namespace laget.Fingerprint.Tests
{
    public class FingerprintManagerTests
    {
        private readonly IFingerprintManager<Models.Fingerprint, Models.User> _fingerprintManager;

        private static Models.User User => new Models.User
        {
            Id = 1,
            Firstname = "Jane",
            Lastname = "Doe",
            LastActive = DateTime.MinValue
        };

        public FingerprintManagerTests()
        {
            _fingerprintManager = new FingerprintManager<Models.Fingerprint, Models.User>(new DictionaryStore<Models.Fingerprint>());

            _fingerprintManager.Add(User.Fingerprint);
        }

        [Fact]
        public void ShouldReturnFingerprintIfItExist()
        {
            var actual = _fingerprintManager.Get(User.Fingerprint.Hash);

            Assert.Equal(User.Fingerprint.Hash, actual.Hash);
        }

        [Fact]
        public async Task ShouldReturnFingerprintIfItExistAsync()
        {
            var actual = await _fingerprintManager.GetAsync(User.Fingerprint.Hash);

            Assert.NotNull(actual);
        }

        [Fact]
        public void ShouldReturnTrueIfFingerprintExist()
        {
            var actual = _fingerprintManager.Exists(User.Fingerprint.Hash);

            Assert.True(actual);
        }

        [Fact]
        public async Task ShouldReturnTrueIfFingerprintExistAsync()
        {
            var actual = await _fingerprintManager.ExistsAsync(User.Fingerprint.Hash);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldReturnFalseIfFingerprintDoesNotExist()
        {
            var user = new Models.User
            {
                Id = 2,
                Firstname = "John",
                Lastname = "Doe",
                LastActive = DateTime.Now.AddMonths(-1)
            };

            var actual = _fingerprintManager.Exists(user.Fingerprint.Hash);

            Assert.False(actual);
        }

        [Fact]
        public async Task ShouldReturnFalseIfFingerprintDoesNotExistAsync()
        {
            var user = new Models.User
            {
                Id = 2,
                Firstname = "John",
                Lastname = "Doe",
                LastActive = DateTime.Now.AddMonths(-1)
            };

            var actual = await _fingerprintManager.ExistsAsync(user.Fingerprint.Hash);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldRemoveFingerprintIfItExist()
        {
            _fingerprintManager.Remove(User.Fingerprint.Hash);

            Assert.Empty(_fingerprintManager.Items);
        }
    }
}
