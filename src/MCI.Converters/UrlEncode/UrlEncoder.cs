namespace Miharu.Converters.UrlEncode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// <seealso cref="https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0"/>
    /// <seealso cref="https://tools.ietf.org/html/rfc3986#section-2.1"/>
    /// </summary>
    public static class UrlEncoder
    {
        public static string Encode(string source, Encoding code)
        {
            //            var bytes = code.GetBytes(source);
            var builder = new StringBuilder(source.Length * 3);


            var chars = source.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                var c = chars[i];
                if (ShouldEscape(c))
                {
                    var bytes = code.GetBytes(new char[] { c });

                    for (var j = 0; j < bytes.Length; j++)
                    {
                        builder.Append('%');
                        builder.Append(bytes[j].ToString("X2"));
                    }
                }
                else
                {
                    builder.Append(c);
                }
            }
            /*
                for (var i = 0; i < bytes.Length; i++)
                {
                    builder.Append('%');
                    builder.Append(bytes[i].ToString("X2"));
                }
            */

            return builder.ToString();
        }

        public static string Decode(string source, Encoding code)
        {
            var builder = new StringBuilder();
            var chars = source.ToCharArray();
            var bytes = new List<byte>();
            var len = chars.Length;
            var i = 0;

            while (i < len)
            {
                var c = chars[i];
                if (c == '%')
                {
                    if (len <= i + 2)
                    {
                        throw new ArgumentOutOfRangeException("invalid format.");
                    }

                    var h = chars[i + 1];
                    var l = chars[i + 2];

                    if (!(IsHexDigit(h) && IsHexDigit(l)))
                    {
                        throw new ArgumentOutOfRangeException("source contains invalid character.");
                    }

                    var b = (byte)(GetByteFromHexDigit(h) * 0x10 + GetByteFromHexDigit(l));

                    bytes.Add(b);

                    i += 3;
                    continue;
                }

                if (!ShouldEscape(c))
                {
                    if (0 < bytes.Count)
                    {
                        builder.Append(code.GetChars(bytes.ToArray()));
                        bytes.Clear();
                    }

                    builder.Append(c);
                    i++;
                }

                throw new ArgumentOutOfRangeException("source contains invalid character.");
            }

            if (0 < bytes.Count)
            {
                builder.Append(code.GetChars(bytes.ToArray()));
                bytes.Clear();
            }

            return builder.ToString();
        }



        internal static bool IsHexDigit(char c)
        {
            return ('0' <= c && c <= '9') || ('A' <= c && c <= 'F') || ('a' <= c && c <= 'f');
        }

        internal static byte GetByteFromHexDigit(char c)
        {
            if ('0' <= c && c <= '9')
            {
                return (byte)((byte)c - (byte)'0');
            }

            if ('A' <= c && c <= 'F')
            {
                return (byte)((byte)c - (byte)'A' + 0x0A);
            }

            if ('a' <= c && c <= 'f')
            {
                return (byte)((byte)c - (byte)'a' + 0x0A);
            }

            throw new ArgumentOutOfRangeException("c");
        }

        public static bool ShouldEscape(char c)
        {
            // unreserved  = ALPHA / DIGIT / "-" / "." / "_" / "~"
            if (('0' <= c && c <= '9') || ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z'))
            {
                return false;
            }

            if (c == '-' || c == '.' || c == '_' || c == '~')
            {
                return false;
            }

            return true;
        }
    }
}
