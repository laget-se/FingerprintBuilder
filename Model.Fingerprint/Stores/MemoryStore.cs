using System;
using System.Collections.Concurrent;

namespace Model.Fingerprint.Stores
{
    public class MemoryStore : IStore
    {
        readonly ConcurrentDictionary<DateTime, DateTime> _dictionary;

        public MemoryStore()
        {
            _dictionary = new ConcurrentDictionary<DateTime, DateTime>();
        }
        public void Add(DateTime dateTime)
        {
            _dictionary.TryAdd(dateTime, dateTime);
        }

        public void Remove(DateTime dateTime)
        {
            _dictionary.TryRemove(dateTime, out _);
        }
    }
}
