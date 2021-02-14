using System;

namespace laget.Fingerprint.Models
{
    public abstract class Fingerprint
    {
        public abstract object Data { get; set; }
        public abstract string Hash { get; set; }
        public abstract object Metadata { get; set; }

        public abstract DateTime CreatedAt { get; set; }
    }
}
