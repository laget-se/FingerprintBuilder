using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using laget.Fingerprint.Interfaces;

namespace laget.Fingerprint.Stores
{
    public class MemoryStore<TFingerprint> : IStore<TFingerprint> where TFingerprint : IFingerprint
    {
        private readonly ConcurrentDictionary<string, TFingerprint> _dictionary;

        public MemoryStore()
        {
            _dictionary = new ConcurrentDictionary<string, TFingerprint>();
        }

        public void Add(TFingerprint model)
        {
            _dictionary.TryAdd(model.Hash, model);
        }

        public async Task AddAsync(TFingerprint model)
        {
           await Task.Run(() => Add(model));
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

        public async Task<TFingerprint> GetAsync(string hash)
        {
            return await Task.Run(() => Get(hash));
        }

        public void Remove(string hash)
        {
            _dictionary.TryRemove(hash, out _);
        }

        public async Task RemoveAsync(string hash)
        {
            await Task.Run(() => RemoveAsync(hash));
        }

        public bool Exists(string hash)
        {
            return _dictionary.ContainsKey(hash);
        }

        public async Task<bool> ExistsAsync(string hash)
        {
            return await Task.Run(() => ExistsAsync(hash));
        }

        public IEnumerable<TFingerprint> Items => _dictionary.Values;
    }
}
