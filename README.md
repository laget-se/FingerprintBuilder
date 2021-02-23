# laget.Fingerprint
Calculates a fingerprint (hash) for an object that can be stored in Memory or a persistent data store.

![Nuget](https://img.shields.io/nuget/v/laget.Fingerprint)
![Nuget](https://img.shields.io/nuget/dt/laget.Fingerprint)

## Configuration
> This example is shown using Autofac since this is the go-to IoC for us.
```c#
public class FingerprintModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Fingerprint>().As<IFingerprint>();

        builder.Register<IFingerprintManager<Fingerprint, User>>(c =>
            new FingerprintManager<Fingerprint, User>(new DictionaryStore<Fingerprint>())
        ).SingleInstance();
    }
}
```

## Usage
### Model
> Since we do not supply a default implementation of the fingerprint model you need to implement one yourself!
```c#
public class Fingerprint : IFingerprint
{
    public string Hash { get; set; }

    public object Data { get; set; }
    public object Metadata { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;


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
```

### Object
> This is the object which you like to fingerprint!
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
