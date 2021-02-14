using laget.Fingerprint.Interfaces;

namespace laget.Fingerprint.Stores
{
    public interface IStore
    {
        void Add(IFingerprint model);
        IFingerprint Get(string hash);
        void Remove(string hash);
        bool Exists(string hash);
    }
}
