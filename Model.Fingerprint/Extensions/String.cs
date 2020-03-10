using System;
using System.Globalization;
using System.Linq;

namespace Fingerprint.Extensions
{
    public static class String
    {
        /// <summary>
        ///     Convert to LowerCase Hexadecimal string
        /// </summary>
        public static string ToLowerHexString(this byte[] source)
        {
            return source.ToString("x2");
        }

        /// <summary>
        ///     Convert to LowerCase Hexadecimal string
        /// </summary>
        public static string ToUpperHexString(this byte[] source)
        {
            return source.ToString("X2");
        }

        /// <summary>
        ///     Convert to string
        /// </summary>
        static string ToString(this byte[] source, string format)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return string.Join("", source.Select(ch => ch.ToString(format, CultureInfo.InvariantCulture)));
        }
    }
}
