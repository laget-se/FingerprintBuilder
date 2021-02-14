using System;
using System.Security.Cryptography;
using laget.Fingerprint.Extensions;
using laget.Fingerprint.Interfaces;

namespace laget.Fingerprint.Tests.Models
{
    public class User : IFingerprintable
    {
        private readonly Func<User, byte[]> _fingerprintBuilder;

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime LastActive { get; set; } = DateTime.Now;

        public User()
        {
            _fingerprintBuilder = FingerprintBuilder<User>
                .Create(SHA512.Create().ComputeHash)
                .For(x => x.Id)
                .For(x => x.Firstname)
                .For(x => x.Lastname)
                .Build();
        }

        public IFingerprint Fingerprint => new Fingerprint
        {
            Hash = _fingerprintBuilder(this).ToUpperHexString(),
            Data = new
            {
                Id,
                Firstname,
                Lastname
            },
            Metadata = new
            {
                LastActive
            }
        };
    }


    public class ExtendedUser : User
    {
        public string[] Emails { get; set; }
    }
}
