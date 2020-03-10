using System;

namespace Model.Fingerprint.Stores
{
    public interface IStore
    {
        void Add(DateTime dateTime);
        void Remove(DateTime dateTime);
    }
}
