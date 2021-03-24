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

## Benchmarks
```ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen Threadripper 3960X, 1 CPU, 12 logical and 12 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 3.1.13 (CoreCLR 4.700.21.11102, CoreFX 4.700.21.11602), X64 RyuJIT
  Job-IQNQYN : .NET Core 3.1.13 (CoreCLR 4.700.21.11102, CoreFX 4.700.21.11602), X64 RyuJIT

Runtime=.NET Core 3.1  IterationCount=50  LaunchCount=2
RunStrategy=Throughput  WarmupCount=10
```

|              Method |     Mean |     Error |    StdDev |   Median |      Min |      Max |
|-------------------- |---------:|----------:|----------:|---------:|---------:|---------:|
|    MD5_Hex | 4.309 us | 0.0170 us | 0.0474 us | 4.298 us | 4.242 us | 4.486 us |
|   SHA1_Hex | 4.540 us | 0.0153 us | 0.0415 us | 4.544 us | 4.403 us | 4.636 us |
| SHA256_Hex | 4.879 us | 0.0171 us | 0.0494 us | 4.876 us | 4.753 us | 5.011 us |
| SHA512_Hex | 7.278 us | 0.1210 us | 0.3471 us | 7.142 us | 6.868 us | 8.219 us |