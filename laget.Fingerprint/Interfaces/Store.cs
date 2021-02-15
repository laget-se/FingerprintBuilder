using System.Collections.Generic;
using System.Threading.Tasks;

namespace laget.Fingerprint.Interfaces
{
    public interface IStore<TFingerprint> where TFingerprint : IFingerprint
    {
        void Add(TFingerprint model);
        Task AddAsync(TFingerprint model);
        TFingerprint Get(string hash);
        Task<TFingerprint> GetAsync(string hash);
        void Remove(string hash);
        Task RemoveAsync(string hash);
        bool Exists(string hash);
        Task<bool> ExistsAsync(string hash);

        IEnumerable<TFingerprint> Items { get; }
    }
}
