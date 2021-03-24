using System.Security.Cryptography;
using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
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

            Assert.Equal("46197041b3a64b3c71f3d1813a0b7a478a3a7a92", hash);
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

            Assert.Equal("472ea61f0548a37904f0c95afa526e87061d8e49", hash);
        }
    }
}
