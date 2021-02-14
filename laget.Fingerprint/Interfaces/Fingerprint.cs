using System;

namespace laget.Fingerprint.Interfaces
{
    public interface IFingerprint
    {
        object Data { get; set; }
        string Hash { get; set; }
        object Metadata { get; set; }

        DateTime CreatedAt { get; set; }
    }

    public class Fingerprint
    {
        public virtual object Data { get; set; }
        public virtual string Hash { get; set; }
        public virtual object Metadata { get; set; }

        public virtual DateTime CreatedAt { get; set; }
    }
}
