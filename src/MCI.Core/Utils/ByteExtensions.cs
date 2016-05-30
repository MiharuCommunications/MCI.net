namespace Miharu.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ByteExtensions
    {
        /// <summary>
        /// 文字列を byte 配列に変換します
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Try<byte[]> ToByteArray(this string source)
        {
            if ((object)source == null)
            {
                return Try<byte[]>.Fail(new NullReferenceException());
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                return Try<byte[]>.Success(new byte[0]);
            }

            var len = source.Length;

            if (len % 2 != 0)
            {
                return Try<byte[]>.Fail(new FormatException());
            }

            var max = len / 2;
            var result = new byte[max];

            for (var i = 0; i < max; i++)
            {
                var up = source[i * 2].AsBCDOpt();
                var dn = source[i * 2 + 1].AsBCDOpt();

                if (up.IsEmpty || dn.IsEmpty)
                {
                    return Try<byte[]>.Fail(new FormatException());
                }
                else
                {
                    result[i] = (byte)(up.Get() * 0x10 + dn.Get());
                }
            }

            return Try<byte[]>.Success(result);
        }



        public static bool IsHex(this char c)
        {
            return ('0' <= c && c <= '9') || ('A' <= c && c <= 'F') || ('a' <= c && c <= 'f');
        }


        public static int AsBCD(this char c)
        {
            if ('0' <= c && c <= '9')
            {
                return (byte)c - (byte)'0';
            }

            if ('A' <= c && c <= 'F')
            {
                return (byte)c - (byte)'A' + 0x0A;
            }

            if ('a' <= c && c <= 'f')
            {
                return (byte)c - (byte)'a' + 0x0A;
            }

            throw new ArgumentOutOfRangeException();
        }


        public static Option<int> AsBCDOpt(this char c)
        {
            if ('0' <= c && c <= '9')
            {
                return Option<int>.Return((byte)c - (byte)'0');
            }

            if ('A' <= c && c <= 'F')
            {
                return Option<int>.Return((byte)c - (byte)'A' + 0x0A);
            }

            if ('a' <= c && c <= 'f')
            {
                return Option<int>.Return((byte)c - (byte)'a' + 0x0A);
            }

            return Option<int>.Fail();
        }


        /// <summary>
        /// byte 配列を 16 進数の文字列に変換します。
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);

            foreach (var b in bytes)
            {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }


        /// <summary>
        /// 16進数で表記した数値を10進数として読み出します。
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Option<int> ReadHexadecimalAsDecimalInt(this byte b)
        {
            if (0 <= b)
            {
                var up = b / 0x10;
                var down = b % 0x10;
                if (up < 10 && down < 10)
                {
                    return Option<int>.Return(up * 10 + down);
                }
            }

            return Option<int>.Fail();
        }
    }
}
