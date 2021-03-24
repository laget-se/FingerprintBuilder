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
    public class Sha256Benchmarks
    {
        private Func<User, byte[]> _sha256;

        private User _user;

        [GlobalSetup]
        public void Setup()
        {
            _user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _sha256 = FingerprintBuilder<User>
                .Create(SHA256.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();
        }

        [Benchmark]
        public string SHA256_Hex()
        {
            return _sha256(_user).ToLowerHexString();
        }
    }
}
