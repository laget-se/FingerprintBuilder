using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace laget.Fingerprint
{
    public interface IFingerprintBuilder<T>
    {
        IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression);

        IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, string>> fingerprint);

        IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, TProperty>> fingerprint);

        Func<T, byte[]> Build();
    }

    public class FingerprintBuilder<T> : IFingerprintBuilder<T>
    {
        private readonly Func<byte[], byte[]> _computeHash;

        private readonly IDictionary<string, Func<T, object>> _fingerprints;

        internal FingerprintBuilder(Func<byte[], byte[]> computeHash)
        {
            _computeHash = computeHash ?? throw new ArgumentNullException(nameof(computeHash));
            _fingerprints = new SortedDictionary<string, Func<T, object>>(StringComparer.OrdinalIgnoreCase);
        }

        public static IFingerprintBuilder<T> Create(Func<byte[], byte[]> computeHash) =>
            new FingerprintBuilder<T>(computeHash);

        public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression) =>
            For<TProperty>(expression, _ => _);

        public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, string>> fingerprint) =>
            For<TProperty, string>(expression, fingerprint);

        public IFingerprintBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, TProperty>> fingerprint) =>
            For<TProperty, TProperty>(expression, fingerprint);

        private IFingerprintBuilder<T> For<TProperty, TPropertyType>(Expression<Func<T, TProperty>> expression, Expression<Func<TProperty, TPropertyType>> fingerprint)
        {
            if (!(expression.Body is MemberExpression memberExpression))
                throw new ArgumentException("Expression must be a member expression");
            if (_fingerprints.ContainsKey(memberExpression.Member.Name))
                throw new ArgumentException($"Member {memberExpression.Member.Name} has already been added.");
            if (!_supportedTypes.Contains(typeof(TPropertyType)))
                throw new ArgumentException($"Unsupported Type: {typeof(TPropertyType).Name}");

            var getValue = expression.Compile();
            var getFingerprint = fingerprint.Compile();

            _fingerprints[memberExpression.Member.Name] = obj =>
            {
                var value = getValue(obj);
                return value == null ? default : getFingerprint(value);
            };

            return this;
        }

        public Func<T, byte[]> Build()
        {
            return obj =>
            {
                using (var memory = new MemoryStream())
                using (var writer = new BinaryWriter(memory))
                {
                    foreach (var item in _fingerprints)
                    {
                        var value = item.Value(obj);
                        switch (value)
                        {
                            case bool typed:
                                writer.Write(typed);
                                break;
                            case byte typed:
                                writer.Write(typed);
                                break;
                            case sbyte typed:
                                writer.Write(typed);
                                break;
                            case byte[] typed:
                                writer.Write(typed);
                                break;
                            case char typed:
                                writer.Write(typed);
                                break;
                            case char[] typed:
                                writer.Write(typed);
                                break;
                            case double typed:
                                writer.Write(typed);
                                break;
                            case decimal typed:
                                writer.Write(typed);
                                break;
                            case short typed:
                                writer.Write(typed);
                                break;
                            case ushort typed:
                                writer.Write(typed);
                                break;
                            case int typed:
                                writer.Write(typed);
                                break;
                            case uint typed:
                                writer.Write(typed);
                                break;
                            case long typed:
                                writer.Write(typed);
                                break;
                            case ulong typed:
                                writer.Write(typed);
                                break;
                            case float typed:
                                writer.Write(typed);
                                break;
                            case string typed:
                                writer.Write(typed);
                                break;
                            case string[] typed:
                                writer.Write(string.Join("", typed));
                                break;
                        }
                    }
                    var arr = memory.ToArray();

                    lock (_computeHash)
                        return _computeHash(arr);
                }
            };
        }

        private readonly Type[] _supportedTypes =
        {
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(byte[]),
            typeof(char),
            typeof(char[]),
            typeof(string),
            typeof(string[]),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong)
        };
    }
}