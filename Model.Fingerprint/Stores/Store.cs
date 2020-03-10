using System;

namespace Fingerprint.Stores
{
    public interface IStore
    {
        void Add(DateTime dateTime);
        void Remove(DateTime dateTime);
    }
}
