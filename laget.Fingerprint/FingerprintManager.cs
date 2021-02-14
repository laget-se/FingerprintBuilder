using laget.Fingerprint.Models;
using laget.Fingerprint.Stores;

namespace laget.Fingerprint
{
    public interface IFingerprintManager<in T> where T : IFingerprintable
    {
        void Add(Models.Fingerprint model);
        Models.Fingerprint Get(string hash);
        void Remove(string hash);
        bool Exists(string hash);
    }

    public class FingerprintManager<T> : IFingerprintManager<T> where T : IFingerprintable
    {
        private readonly IStore _store;

        public FingerprintManager(IStore store)
        {
            _store = store;
        }

        public void Add(Models.Fingerprint model)
        {
            _store.Add(model);
        }

        public Models.Fingerprint Get(string hash)
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
    }
}
