using System;
using System.Linq.Expressions;

namespace Fingerprint.Extensions
{
    public static class Expression
    {
        public static IFingerprintBuilder<T> For<T>(this IFingerprintBuilder<T> builder, Expression<Func<T, string>> expression, bool toLowerCase, bool ignoreWhiteSpace)
        {
            var format = (Func<string, string>)(input =>
            {
                if (toLowerCase)
                    input = input.ToLowerInvariant();

                if (ignoreWhiteSpace)
                    input = input.Trim();

                return input;
            });

            return builder.For(expression, input => format(input));
        }
    }
}
