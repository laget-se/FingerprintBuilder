using System.Security.Cryptography;
using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
using Xunit;

namespace laget.Fingerprint.Tests
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

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe"
            };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("a4aca7cce7e349205f08a25a38267ffcf05c60ed", hash);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha1()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname, true, true)
                .For(p => p.Lastname, true, true)
                .Build();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe"
            };

            const string expectedHash = "b40d1e3afbe61730fa1daae2a539ad730ea09b0a";

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

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe"
            };

            var hashLower = fingerprint(user).ToLowerHexString();
            var hashUpper = fingerprint(user).ToUpperHexString();

            Assert.Equal("00b93d68198f13544cf71b96b7dcfc29a33014d20c0878cc434617482b65d2d2", hashLower);
            Assert.Equal("00b93d68198f13544cf71b96b7dcfc29a33014d20c0878cc434617482b65d2d2".ToUpperInvariant(), hashUpper);
        }

        [Fact]
        public void UserInfo_ToLowerCase_Sha256()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.Firstname, true, true)
                .For(p => p.Lastname, true, true)
                .Build();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe"
            };

            const string expectedHash = "d53e54efc3c01db03d6494f95880d53349951a3af3fd6b1fd9459605ad3baeb7";

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

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe"
            };

            var hashLower = fingerprint(user).ToLowerHexString();
            var hashUpper = fingerprint(user).ToUpperHexString();

            Assert.Equal("bc09471d63bdb0604a30cb0de118ec2b48153a1b7fe813fcad0974c44094b0a480bc3d158b52a6842756994c437afb7742d973371be440a46c2b5cfeb5d7dcd6", hashLower);
            Assert.Equal("bc09471d63bdb0604a30cb0de118ec2b48153a1b7fe813fcad0974c44094b0a480bc3d158b52a6842756994c437afb7742d973371be440a46c2b5cfeb5d7dcd6".ToUpperInvariant(), hashUpper);
        }

        [Fact]
        public void UserInfo_Sha1_Null()
        {
            var fingerprint = FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .Build();

            var user = new User
            {
                Firstname = "John",
                Lastname = null
            };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("5ab5aeba11346413348fb7c9361058e016ecf3ca", hash);
        }
    }
}
