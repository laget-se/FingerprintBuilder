using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
using System.Security.Cryptography;
using Xunit;

namespace laget.Fingerprint.Tests
{
    public class ArrayFingerprintBuilderTests
    {
        [Fact]
        public void UserInfo_Sha1()
        {
            var fingerprint = FingerprintBuilder<ExtendedUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .For(p => p.Emails)
                .Build();

            var user = new ExtendedUser
            {
                Firstname = "John",
                Lastname = "Doe",
                Emails = new[] { "test@test.com", "test1@test.com" }
            };

            var hash = fingerprint(user).ToLowerHexString();

            const string expectedHash = "574458180eb2cfee226d35de6f346d99549f3d09";

            Assert.Equal(expectedHash, hash);
        }

        [Fact]
        public void UserInfo_EmailsString_Sha1()
        {
            var fingerprint = FingerprintBuilder<ExtendedUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .For(p => p.Emails, emails => string.Join("|", emails))
                .Build();

            var user = new ExtendedUser
            {
                Firstname = "John",
                Lastname = "Doe",
                Emails = new[] { "test@test.com", "test1@test.com" }
            };

            var hash = fingerprint(user).ToLowerHexString();

            const string expectedHash = "0ea8484c3503de7209753aa11fe8321181298e7a";

            Assert.Equal(expectedHash, hash);
        }
    }
}
