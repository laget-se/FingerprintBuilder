using laget.Fingerprint.Extensions;
using laget.Fingerprint.Tests.Models;
using System;
using System.Security.Cryptography;
using Xunit;

namespace laget.Fingerprint.Tests.Types
{
    public class DoubleTests
    {
        private readonly Func<ThisUser, byte[]> _sha1;
        private readonly ThisUser _user;

        public DoubleTests()
        {
            _sha1 = FingerprintBuilder<ThisUser>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Number)
                .Build();

            _user = new ThisUser
            {
                Firstname = "John",
                Number = 2.1d
            };
        }

        [Fact]
        public void Sha1()
        {
            var hash = _sha1(_user).ToLowerHexString();

            Assert.Equal("af167d0996599a29b89ce1c467013bb7c98e7dcb", hash);
        }

        [Fact]
        public void UserInfo_Sha1_UpdateBool_ChangeHash()
        {
            var hash0 = _sha1(_user).ToLowerHexString();
            _user.Number = 2.11d;
            var hash1 = _sha1(_user).ToLowerHexString();

            Assert.NotEqual(hash0, hash1);
        }

        private class ThisUser : User
        {
            public double Number { get; set; }
        }
    }
}
