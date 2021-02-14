using System.Collections.Generic;
using laget.Fingerprint.Interfaces;
using laget.Fingerprint.Stores;

namespace laget.Fingerprint
{
    public interface IFingerprintManager<in T> where T : IFingerprintable
    {
        void Add(IFingerprint model);
        IFingerprint Get(string hash);
        void Remove(string hash);
        bool Exists(string hash);

        IEnumerable<IFingerprint> Items { get; }
    }

    public class FingerprintManager<T> : IFingerprintManager<T> where T : IFingerprintable
    {
        private readonly IStore _store;

        public FingerprintManager(IStore store)
        {
            _store = store;
        }

        public void Add(IFingerprint model)
        {
            _store.Add(model);
        }

        public IFingerprint Get(string hash)
        {
            return _store.Get(hash);
        }

        public void Remove(string hash)
        {
            _store.Remove(hash);
        }

        public bool Exists(string hash)
        {
            return _store.Exists(hash);
        }

        public IEnumerable<IFingerprint> Items => _store.Items;
    }
}
