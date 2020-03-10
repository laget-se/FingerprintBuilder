using System.Security.Cryptography;
using Intercom.Service.FingerprintBuilder;
using Intercom.Service.FingerprintBuilder.Tests.Models;
using Xunit;

namespace FingerprintBuilder.Tests
{
    public class SimpleFingerprintBuilderTests
    {
        [Fact]
        public void UserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .Build();

            var user = new User { Firstname = "John", Lastname = "Smith" };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("bfe2cb034d9448e66f642506e6370dd87bbbe0e0", hash);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha1()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname, true, true)
                .For(p => p.Lastname, true, true)
                .Build();

            var user = new User { Firstname = "John", Lastname = "Smith" };

            const string expectedHash = "df7fd58e2378573dd2e6e7340a9b2390d2bda770";

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal(expectedHash, hash);

            user.Firstname = user.Firstname.ToLowerInvariant();
            user.Lastname = user.Lastname.ToLowerInvariant();

            var hash1 = fingerprint(user).ToLowerHexString();
            Assert.Equal(expectedHash, hash1);
        }

        [Fact]
        public void UserInfo_Sha256()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .Build();

            var user = new User { Firstname = "John", Lastname = "Smith" };

            var hashLower = fingerprint(user).ToLowerHexString();
            var hashUpper = fingerprint(user).ToUpperHexString();

            Assert.Equal("9996c4bbc1da4938144886b27b7c680e75932b5a56d911754d75ae4e0a9b4f1a", hashLower);
            Assert.Equal("9996c4bbc1da4938144886b27b7c680e75932b5a56d911754d75ae4e0a9b4f1a".ToUpperInvariant(), hashUpper);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha256()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.Firstname, true, true)
                .For(p => p.Lastname, true, true)
                .Build();

            var user = new User { Firstname = "John", Lastname = "Smith" };

            const string expectedHash = "6012fe3d8bd3038b701c9ddec210b591baecc3aa2ec1f727a7d1f3c9f2032cb3";

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal(expectedHash, hash);

            user.Firstname = user.Firstname.ToLowerInvariant();
            user.Lastname = user.Lastname.ToLowerInvariant();

            var hash1 = fingerprint(user).ToLowerHexString();
            Assert.Equal(expectedHash, hash1);
        }

        [Fact]
        public void UserInfo_Sha512()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA512.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .Build();

            var user = new User { Firstname = "John", Lastname = "Smith" };

            var hashLower = fingerprint(user).ToLowerHexString();
            var hashUpper = fingerprint(user).ToUpperHexString();

            Assert.Equal("628aa41f90dac147e4366e7fd9ce68ffb9d8a73b7a6e502e8f9353909a4248786f2cfa2b192588e89cb20561006a3d41a360732eb1b8a802f44621a7cea39112", hashLower);
            Assert.Equal("628aa41f90dac147e4366e7fd9ce68ffb9d8a73b7a6e502e8f9353909a4248786f2cfa2b192588e89cb20561006a3d41a360732eb1b8a802f44621a7cea39112".ToUpperInvariant(), hashUpper);
        }

        [Fact]
        public void UserInfo_Sha1_Null()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .Build();

            var user = new User { Firstname = "John", Lastname = null };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("5ab5aeba11346413348fb7c9361058e016ecf3ca", hash);
        }
    }
}
