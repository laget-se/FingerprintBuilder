using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using laget.Fingerprint.Benchmarks.Models;
using laget.Fingerprint.Extensions;
using System;
using System.Security.Cryptography;

namespace laget.Fingerprint.Benchmarks.Benchmarks
{
    [MeanColumn, MinColumn, MaxColumn, MedianColumn]
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.Net80, 2, 10, 50)]
    public class Md5Benchmarks
    {
        private Func<User, byte[]> _md5;

        private User _user;

        [GlobalSetup]
        public void Setup()
        {
            _user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _md5 = FingerprintBuilder<User>
                .Create(MD5.Create().ComputeHash)
                .For(p => p.FirstName)
                .For(p => p.LastName)
                .Build();
        }

        [Benchmark]
        public string MD5_Hex()
        {
            return _md5(_user).ToLowerHexString();
        }
    }
}
