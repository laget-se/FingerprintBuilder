namespace laget.Fingerprint.Models
{
    public interface IFingerprintable
    {
        Fingerprint ToFingerprint { get; }
        bool HasChanged { get; }
    }
}
