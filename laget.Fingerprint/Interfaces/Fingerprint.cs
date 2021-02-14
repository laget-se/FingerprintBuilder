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

    public class Fingerprint : IFingerprint
    {
        public virtual object Data { get; set; }
        public virtual string Hash { get; set; }
        public virtual object Metadata { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is IFingerprint))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }
            var fingerprint = (IFingerprint)obj;
            if (Hash == null)
            {
                return fingerprint.Hash == null;
            }
            return Hash.Equals(fingerprint.Hash, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }

        public static bool operator ==(Fingerprint lhs, Fingerprint rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return ReferenceEquals(rhs, null);
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Fingerprint lhs, Fingerprint rhs)
        {
            return !(lhs == rhs);
        }
    }
}
