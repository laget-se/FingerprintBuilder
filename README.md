# laget.Fingerprint
Calculates a fingerprint (hash) for an object that can be stored in Memory or a persistent data store.

![Nuget](https://img.shields.io/nuget/v/laget.Fingerprint)
![Nuget](https://img.shields.io/nuget/dt/laget.Fingerprint)

## Usage
```c#
public class User : IFingerprintable
{
    private readonly Func<User, byte[]> _fingerprintBuilder;

    public User()
    {
        _fingerprintBuilder = FingerprintBuilder<User>
            .Create(SHA512.Create().ComputeHash)
            .For(x => x.Id)
            .For(x => x.Firstname)
            .For(x => x.Lastname)
            .For(x => x.Email)
            .Build();
    }

    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public DateTime LastActive { get; set; } = DateTime.Now;

    public IFingerprint Fingerprint => new Fingerprint
    {
        Hash = _fingerprintBuilder(this).ToUpperHexString(),
        Data = new
        {
            Id,
            Firstname,
            Lastname,
            Email
        },
        Metadata = new
        {
            LastActive
        }
    };
}
```
