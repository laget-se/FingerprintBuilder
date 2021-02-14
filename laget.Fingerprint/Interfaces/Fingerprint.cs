using System;

namespace laget.Fingerprint.Interfaces
{
    public interface IFingerprint
    {
        object Data { get; set; }
        string Hash { get; set; }
        object Metadata { get; set; }

        DateTime CreatedAt { get; set; }

        bool Equals(object obj);
    }
}
