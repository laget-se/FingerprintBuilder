using laget.Fingerprint.Tests.Models;
using System;
using System.Security.Cryptography;
using Xunit;

namespace laget.Fingerprint.Tests
{
    public class WrongBuildTests
    {
        [Fact]
        public void ComputeHash_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FingerprintBuilder<User>.Create(computeHash: null));
        }

        [Fact]
        public void HashAlgorithm_Null_Throw_ArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => FingerprintBuilder<User>.Create(computeHash: null));
        }

        [Fact]
        public void For_Prop_Duplicate_Throw_ArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.Firstname)
                .For(p => p.Firstname));

            Assert.Equal(nameof(User.Firstname), exception.ParamName);
        }

        [Fact]
        public void For_Prop_IsNot_MemberExpression_Throw_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => ""));
        }
    }
}
