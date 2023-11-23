using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
using System.Security.Cryptography;
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

            const string expectedHash = "1992cac23b505a626e03a3cc7ec6665b0ed5f2db";

            Assert.Equal(expectedHash, hash);
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

            var hash = fingerprint(user).ToLowerHexString();
            user.Firstname = user.Firstname.ToLowerInvariant();
            user.Lastname = user.Lastname.ToLowerInvariant();
            var hash1 = fingerprint(user).ToLowerHexString();

            const string expectedHash = "6e7b222a846fa6033d2555b1f3301fe9932dad05";

            Assert.Equal(expectedHash, hash);
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

            const string expectedHash = "21da793524932fea997f7054bbd77a99cd2ee153199740be129f868c1134dc74";

            Assert.Equal(expectedHash, hashLower);
            Assert.Equal(expectedHash.ToUpperInvariant(), hashUpper);
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

            var hash = fingerprint(user).ToLowerHexString();
            user.Firstname = user.Firstname.ToLowerInvariant();
            user.Lastname = user.Lastname.ToLowerInvariant();
            var hash1 = fingerprint(user).ToLowerHexString();

            const string expectedHash = "c1919a2bb337408cc657e13ce969455ad8799923d6641d2d1fabe8fe7bc9d943";

            Assert.Equal(expectedHash, hash);
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

            const string expectedHash = "59be4f006778ec18d8c6ddfe7068f113711c8f279afc4f9f287eaf0857c125de0b44e1c8bca86dedc4491294c69014d911545f9c9e04c37c9a8a9396a97c5a56";

            Assert.Equal(expectedHash, hashLower);
            Assert.Equal(expectedHash.ToUpperInvariant(), hashUpper);
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

            const string expectedHash = "2e2a2813f314668223c79f0bc819b39ce71810ca";

            Assert.Equal(expectedHash, hash);
        }
    }
}
