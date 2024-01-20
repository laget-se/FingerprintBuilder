using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
using System;
using System.Security.Cryptography;
using Xunit;

namespace laget.Fingerprint.Tests.Types
{
    public class DecimalTests
    {
        private readonly Func<ThisUser, byte[]> _sha1;
        private readonly ThisUser _user;

        public DecimalTests()
        {
            _sha1 = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Number)
                .Build();

            _user = new ThisUser
            {
                Firstname = "John",
                Number = 2.1m
            };
        }

        [Fact]
        public void Sha1()
        {
            var hash = _sha1(_user).ToLowerHexString();

            Assert.Equal("0e9124882db525d318aaad3047f579978f5b0fbb", hash);
        }

        [Fact]
        public void UserInfo_Sha1_UpdateBool_ChangeHash()
        {
            var hash0 = _sha1(_user).ToLowerHexString();
            _user.Number = 2.11m;
            var hash1 = _sha1(_user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class ThisUser : User
        {
            public decimal Number { get; set; }
        }
    }
}
