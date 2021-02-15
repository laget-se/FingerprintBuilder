using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using laget.Fingerprint.Interfaces;

namespace laget.Fingerprint.Stores
{
    public class DictionaryStore<TFingerprint> : IStore<TFingerprint> where TFingerprint : IFingerprint
    {
        private readonly ConcurrentDictionary<string, TFingerprint> _dictionary;

        public DictionaryStore()
        {
            _dictionary = new ConcurrentDictionary<string, TFingerprint>();
        }

        public void Add(TFingerprint model)
        {
            _dictionary.TryAdd(model.Hash, model);
        }

        public Task AddAsync(TFingerprint model)
        {
            throw new NotImplementedException();
        }

        public TFingerprint Get(string hash)
        {
            try
            {
                _dictionary.TryGetValue(hash, out var fingerprint);

                return fingerprint;
            }
            catch (ArgumentNullException)
            {
                return default;
            }
        }

        public Task<TFingerprint> GetAsync(string hash)
        {
            throw new NotImplementedException();
        }

        public void Remove(string hash)
        {
            _dictionary.TryRemove(hash, out _);
        }

        public Task RemoveAsync(string hash)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string hash)
        {
            return _dictionary.ContainsKey(hash);
        }

        public Task<bool> ExistsAsync(string hash)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TFingerprint> Items => _dictionary.Values;
    }
}
