using System;
using System.Collections.Concurrent;

namespace laget.Fingerprint.Stores
{
    public class MemoryStore : IStore
    {
        private readonly ConcurrentDictionary<string, Models.Fingerprint> _dictionary;

        public MemoryStore()
        {
            _dictionary = new ConcurrentDictionary<string, Models.Fingerprint>();
        }
        public void Add(Models.Fingerprint model)
        {
            _dictionary.TryAdd(model.Hash, model);
        }

        public Models.Fingerprint Get(string hash)
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
