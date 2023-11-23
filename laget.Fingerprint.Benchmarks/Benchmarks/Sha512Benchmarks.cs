using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using laget.Fingerprint.Benchmarks.Models;
using laget.Fingerprint.Extensions;
using System;
using System.Security.Cryptography;

namespace laget.Fingerprint.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    [MeanColumn, MinColumn, MaxColumn, MedianColumn]
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.Net80, 2, 10, 50)]
    public class Sha512Benchmarks
    {
        private Func<User, byte[]> _sha512;

        private User _user;

        [GlobalSetup]
        public void Setup()
        {
            _user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _sha512 = FingerprintBuilder<User>
                .Create(SHA512.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();
        }

        [Benchmark]
        public string SHA512_Hex()
        {
            return _sha512(_user).ToLowerHexString();
        }
    }
}
