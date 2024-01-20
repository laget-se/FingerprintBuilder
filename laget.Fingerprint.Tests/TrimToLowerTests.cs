using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
using System;
using System.Security.Cryptography;
using Xunit;

namespace laget.Fingerprint.Tests
{
    public class TrimToLowerTests
    {
        private readonly Func<User, byte[]> _sha1;

        public TrimToLowerTests()
        {
            _sha1 = FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname, true, true)
                .For(p => p.Lastname, true, true)
                .Build();
        }

        [Fact]
        public void Sha1_ToLower_Trim()
        {
            const string expectedHash = "302ad30676be9e618daed716b3710ab70c1323db";

            var user = new User
            {
                Firstname = "John ",
                Lastname = "Smith "
            };

            var hash = _sha1(user).ToLowerHexString();

            Assert.Equal(expectedHash, hash);

            user.Firstname = user.Firstname.ToLowerInvariant();
            user.Lastname = user.Lastname.ToLowerInvariant();

            var hash1 = _sha1(user).ToLowerHexString();
            Assert.Equal(expectedHash, hash1);
        }

        [Fact]
        public void Sha256_ToLower_Trim()
        {
            var sha256 = FingerprintBuilder<User>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.Firstname, true, true)
                .For(p => p.Lastname, true, true)
                .Build();

            const string expectedHash = "fdd11c24f2c3f4cd9e57fbbdf77aa4c3332959fda1f6097a92d6e212aa2a533f";
            var user = new User
            {
                Firstname = "John",
                Lastname = "Smith "
            };

            var hash = sha256(user).ToLowerHexString();

            Assert.Equal(expectedHash, hash);

            user.Firstname = user.Firstname.ToLowerInvariant();
            user.Lastname = user.Lastname.ToLowerInvariant();

            var hash1 = sha256(user).ToLowerHexString();
            Assert.Equal(expectedHash, hash1);
        }
    }
}
