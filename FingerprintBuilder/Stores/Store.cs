using System;

namespace FingerprintBuilder.Stores
{
    public interface IStore
    {
        void Add(DateTime dateTime);
        void Remove(DateTime dateTime);
    }
}
