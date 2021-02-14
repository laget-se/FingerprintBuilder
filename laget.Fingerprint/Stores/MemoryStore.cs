using System;
using System.Collections.Concurrent;
using laget.Fingerprint.Interfaces;

namespace laget.Fingerprint.Stores
{
    public class MemoryStore : IStore
    {
        private readonly ConcurrentDictionary<string, IFingerprint> _dictionary;

        public MemoryStore()
        {
            _dictionary = new ConcurrentDictionary<string, IFingerprint>();
        }
        public void Add(IFingerprint model)
        {
            _dictionary.TryAdd(model.Hash, model);
        }

        public IFingerprint Get(string hash)
        {
            try
            {
                _dictionary.TryGetValue(hash, out var fingerprint);

                return fingerprint;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public void Remove(string hash)
        {
            _dictionary.TryRemove(hash, out _);
        }

        public bool Exists(string hash)
        {
            return _dictionary.ContainsKey(hash);
        }
    }
}
