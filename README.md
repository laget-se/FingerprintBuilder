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
BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen Threadripper 3960X, 1 CPU, 48 logical and 24 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-THOOBN : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

Runtime=.NET 8.0  IterationCount=50  LaunchCount=2
RunStrategy=Throughput  WarmupCount=10
```

|              Method |     Mean |     Error |    StdDev |   Median |      Min |      Max |
|-------------------- |---------:|----------:|----------:|---------:|---------:|---------:|
| MD5_Hex | 886.5 ns | 2.87 ns | 8.47 ns | 864.0 ns | 909.2 ns | 886.9 ns |
| SHA1_Hex | 985.2 ns | 3.80 ns | 11.21 ns | 963.1 ns | 1,012.8 ns | 986.1 ns |
| SHA256_Hex | 1.167 us | 0.0053 us | 0.0157 us | 1.135 us | 1.206 us | 1.168 us |
| SHA512_Hex | 2.066 us | 0.0153 us | 0.0452 us | 2.006 us | 2.172 us | 2.054 us |

## Stores
### Mongo
> This example is shown using Autofac since this is the go-to IoC for us.
```c#
public class FingerprintModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<IFingerprintManager<Fingerprint, User>>(c =>
            new FingerprintManager<Fingerprint, User>(new MongoStore<Fingerprint, User>(new MongoUrl(c.Resolve<IConfiguration>().GetConnectionString("MongoConnectionString"))))
        ).SingleInstance();
    }
}
```
