using laget.Fingerprint.Extensions;
using laget.Fingerprint.Interfaces;
using laget.Fingerprint.Stores;
using laget.Fingerprint.Tests.Models;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace laget.Fingerprint.Tests
{
    public class FingerprintManagerTests
    {
        private readonly IFingerprintManager<Models.Fingerprint, ThisUserManager> _fingerprintManager;

        private static ThisUserManager Manager => new ThisUserManager
        {
            Id = 1,
            Firstname = "Jane",
            Lastname = "Doe",
            LastActive = DateTime.MinValue
        };

        public FingerprintManagerTests()
        {
            _fingerprintManager = new FingerprintManager<Models.Fingerprint, ThisUserManager>(new DictionaryStore<Models.Fingerprint>());

            _fingerprintManager.Add(Manager.Fingerprint);
        }

        [Fact]
        public void ShouldReturnFingerprintIfItExist()
        {
            var actual = _fingerprintManager.Get(Manager.Fingerprint.Hash);

            Assert.Equal(Manager.Fingerprint.Hash, actual.Hash);
        }

        [Fact]
        public async Task ShouldReturnFingerprintIfItExistAsync()
        {
            var actual = await _fingerprintManager.GetAsync(Manager.Fingerprint.Hash);

            Assert.NotNull(actual);
        }

        [Fact]
        public void ShouldReturnTrueIfFingerprintExist()
        {
            var actual = _fingerprintManager.Exists(Manager.Fingerprint.Hash);

            Assert.True(actual);
        }

        [Fact]
        public async Task ShouldReturnTrueIfFingerprintExistAsync()
        {
            var actual = await _fingerprintManager.ExistsAsync(Manager.Fingerprint.Hash);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldReturnFalseIfFingerprintDoesNotExist()
        {
            var manager = new ThisUserManager
            {
                Id = 2,
                Firstname = "John",
                Lastname = "Doe",
                LastActive = DateTime.Now.AddMonths(-1)
            };

            var actual = _fingerprintManager.Exists(manager.Fingerprint.Hash);

            Assert.False(actual);
        }

        [Fact]
        public async Task ShouldReturnFalseIfFingerprintDoesNotExistAsync()
        {
            var manager = new ThisUserManager
            {
                Id = 2,
                Firstname = "John",
                Lastname = "Doe",
                LastActive = DateTime.Now.AddMonths(-1)
            };

            var actual = await _fingerprintManager.ExistsAsync(manager.Fingerprint.Hash);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldRemoveFingerprintIfItExist()
        {
            _fingerprintManager.Remove(Manager.Fingerprint.Hash);

            Assert.Empty(_fingerprintManager.Items);
        }

        private class ThisUserManager : User, IFingerprintable
        {
            private readonly Func<User, byte[]> _fingerprintBuilder;

            public ThisUserManager()
            {
                _fingerprintBuilder = FingerprintBuilder<User>
                    .Create(SHA512.Create().ComputeHash)
                    .For(x => x.Id)
                    .For(x => x.Firstname)
                    .For(x => x.Lastname)
                    .Build();
            }

            public IFingerprint Fingerprint => new Models.Fingerprint
            {
                Hash = _fingerprintBuilder(this).ToUpperHexString(),
                Data = new
                {
                    Id,
                    Firstname,
                    Lastname
                },
                Metadata = new
                {
                    LastActive
                }
            };
        }
    }
}
