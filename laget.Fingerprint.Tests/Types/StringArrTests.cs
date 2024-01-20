using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
using System;
using System.Security.Cryptography;
using Xunit;

namespace laget.Fingerprint.Tests.Types
{
    public class StringArrTests
    {
        private readonly Func<ThisUser, byte[]> _sha1;
        private readonly ThisUser _user;

        public StringArrTests()
        {
            _sha1 = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Emails, emails => string.Join("|", emails))
                .Build();
            _user = new ThisUser { Firstname = "John", Emails = new[] { "test@test.com" } };
        }

        [Fact]
        public void EmailsAsArray_ThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FingerprintBuilder<ThisUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Emails)
                .Build());
            Assert.Equal(nameof(ThisUser.Emails), exception.ParamName);
        }

        [Fact]
        public void Sha1_EmailsToString()
        {
            var hash = _sha1(_user).ToLowerHexString();
            Assert.Equal("a9840b53e6aadb3a3a5a48d3ffc9df56441304b6", hash);
        }

        [Fact]
        public void Sha1_EmailsToString_UpdateArray_ChangeHash()
        {
            var hash0 = _sha1(_user).ToLowerHexString();
            _user.Emails[0] += "1";
            var hash1 = _sha1(_user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class ThisUser : User
        {
            public string[] Emails { get; set; }
        }
    }
}
