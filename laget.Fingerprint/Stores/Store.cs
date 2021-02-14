namespace laget.Fingerprint.Stores
{
    public interface IStore
    {
        void Add(Models.Fingerprint model);
        Models.Fingerprint Get(string hash);
        void Remove(string hash);
        bool Exists(string hash);
    }
}
