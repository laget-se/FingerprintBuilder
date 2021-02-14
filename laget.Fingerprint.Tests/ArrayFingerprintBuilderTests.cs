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

            var user = new ExtendedUser { Firstname = "John", Lastname = "Smith", Emails = new[] { "test@test.com", "test1@test.com" } };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal((string) "910247a24d302fbea05d2699ed73ce6c351743f1", (string) hash);
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

            var user = new ExtendedUser { Firstname = "John", Lastname = "Smith", Emails = new[] { "test@test.com", "test1@test.com" } };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal((string) "307de22ecb4eb4c4825cac107d891838dc6c86a7", (string) hash);
        }
    }
}
