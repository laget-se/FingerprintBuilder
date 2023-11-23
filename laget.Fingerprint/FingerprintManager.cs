using laget.Fingerprint.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace laget.Fingerprint
{
    public interface IFingerprintManager<TFingerprint, TEntity>
        where TFingerprint : IFingerprint
    {
        void Add(IFingerprint model);
        Task AddAsync(IFingerprint model);
        TFingerprint Get(string hash);
        Task<TFingerprint> GetAsync(string hash);
        void Remove(string hash);
        Task RemoveAsync(string hash);
        bool Exists(string hash);
        Task<bool> ExistsAsync(string hash);
        bool Exists(IFingerprint model);
        Task<bool> ExistsAsync(IFingerprint model);

        IEnumerable<TFingerprint> Items { get; }
    }

    public class FingerprintManager<TFingerprint, TEntity> : IFingerprintManager<TFingerprint, TEntity>
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

        public async Task AddAsync(IFingerprint model)
        {
            await _store.AddAsync((TFingerprint)model);
        }

        public TFingerprint Get(string hash)
        {
            return _store.Get(hash);
        }

        public async Task<TFingerprint> GetAsync(string hash)
        {
            return await _store.GetAsync(hash);
        }

        public void Remove(string hash)
        {
            _store.Remove(hash);
        }

        public async Task RemoveAsync(string hash)
        {
            await _store.RemoveAsync(hash);
        }

        public bool Exists(string hash)
        {
            return _store.Exists(hash);
        }

        public async Task<bool> ExistsAsync(string hash)
        {
            return await _store.ExistsAsync(hash);
        }

        public bool Exists(IFingerprint model)
        {
            return _store.Exists(model.Hash);
        }

        public async Task<bool> ExistsAsync(IFingerprint model)
        {
            return await _store.ExistsAsync(model.Hash);
        }

        public IEnumerable<TFingerprint> Items => _store.Items;
    }
}
