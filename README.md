# FingerprintBuilder
Calculates a fingerprint (hash) for an object that can be stored in Memory or a persistent data store.

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
            .For(x => x.Forname)
            .For(x => x.Surename)
            .For(x => x.Email)
            .Build();
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public string Forname { get; set; }
    public string Surename { get; set; }
    public string Email { get; set; }

    public string Fingerprint => _fingerprintBuilder(this).ToUpperHexString();
}
```