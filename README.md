# laget.Fingerprint
Calculates a fingerprint (hash) for an object that can be stored in Memory or a persistent data store.

![Nuget](https://img.shields.io/nuget/v/laget.Fingerprint)

## Usage
```c#
public class User
{
    readonly Func<User, byte[]> _fingerprintBuilder;

    public User()
    {
        _fingerprintBuilder = FingerprintBuilder<User>
            .Create(SHA512.Create().ComputeHash)
            .For(x => x.Id)
            .For(x => x.UserId)
            .For(x => x.Forename)
            .For(x => x.Surename)
            .For(x => x.Email)
            .Build();
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public string Forename { get; set; }
    public string Surename { get; set; }
    public string Email { get; set; }

    public string Fingerprint => _fingerprintBuilder(this).ToUpperHexString();
}
```
