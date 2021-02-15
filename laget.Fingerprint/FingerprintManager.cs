using System.Collections.Generic;
using laget.Fingerprint.Interfaces;

namespace laget.Fingerprint
{
    public interface IFingerprintManager<TEntity, TFingerprint>
        where TFingerprint : IFingerprint
    {
        void Add(IFingerprint model);
        TFingerprint Get(string hash);
        void Remove(string hash);
        bool Exists(string hash);

        IEnumerable<TFingerprint> Items { get; }
    }

    public class FingerprintManager<TEntity, TFingerprint> : IFingerprintManager<TEntity, TFingerprint>
        where TEntity : IFingerprintable
        where TFingerprint : IFingerprint
    {
        private readonly IStore<TFingerprint> _store;

        public FingerprintManager(IStore<TFingerprint> store)
        {
            _store = store;
        }

        public void Add(IFingerprint model)
        {
            _store.Add((TFingerprint)model);
        }

        public TFingerprint Get(string hash)
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

        public IEnumerable<TFingerprint> Items => _store.Items;
    }
}
