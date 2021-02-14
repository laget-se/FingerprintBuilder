namespace laget.Fingerprint.Interfaces
{
    public interface IFingerprintable
    {
        IFingerprint ToFingerprint { get; }
        bool HasChanged { get; }
    }
}
