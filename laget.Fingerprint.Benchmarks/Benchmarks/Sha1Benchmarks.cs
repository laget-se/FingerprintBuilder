using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using laget.Fingerprint.Benchmarks.Models;
using laget.Fingerprint.Extensions;

namespace laget.Fingerprint.Benchmarks.Benchmarks
{
    [MeanColumn, MinColumn, MaxColumn, MedianColumn]
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.NetCoreApp31, 2, 10, 50)]
    public class Sha1Benchmarks
    {
        private Func<User, byte[]> _sha1;

        private User _user;

        [GlobalSetup]
        public void Setup()
        {
            _user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _sha1 = FingerprintBuilder<User>
                .Create(SHA1.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();
        }

        [Benchmark]
        public string SHA1_Hex()
        {
            return _sha1(_user).ToLowerHexString();
        }
    }
}
