using System.Security.Cryptography;
using Intercom.Service.FingerprintBuilder.Tests.Models;
using Xunit;

namespace Intercom.Service.FingerprintBuilder.Tests
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

            Assert.Equal("910247a24d302fbea05d2699ed73ce6c351743f1", hash);
        }

        [Fact]
        public void UserInfo_EmailsString_Sha1()
        {
            var fingerprint = FingerprintBuilder<ExtendedUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Lastname)
                .For(p => p.Emails, emails => string.Join('|', emails))
                .Build();

            var user = new ExtendedUser { Firstname = "John", Lastname = "Smith", Emails = new[] { "test@test.com", "test1@test.com" } };

            var hash = fingerprint(user).ToLowerHexString();

            Assert.Equal("307de22ecb4eb4c4825cac107d891838dc6c86a7", hash);
        }
    }
}
