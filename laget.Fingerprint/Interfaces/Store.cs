using System.Collections.Generic;

namespace laget.Fingerprint.Interfaces
{
    public interface IStore<TFingerprint> where TFingerprint : IFingerprint
    {
        void Add(TFingerprint model);
        TFingerprint Get(string hash);
        void Remove(string hash);
        bool Exists(string hash);

        IEnumerable<TFingerprint> Items { get; }
    }
}
