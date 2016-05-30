//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 文字列のための拡張メソッド
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// <para>指定した文字数づつに文字列を分割します。</para>
        /// <para>文字数が丁度でなかった場合、最後の文字列は指定した文字数以下になります。</para>
        /// </summary>
        /// <param name="source">分割する文字列</param>
        /// <param name="count">分割する文字数</param>
        /// <returns>分割された文字列のコレクション</returns>
        /// <exception cref="System.ArgumentNullException">分割される文字列が null の場合</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">分割できない範囲の文字数の場合</exception>
        public static IEnumerable<string> Divide(this string source, int count)
        {
            if ((object)source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("count", "分割は 1 以上の正の整数でしか出来ません。");
            }

            if (source.Length < count)
            {
                yield return source;
                yield break;
            }

            int length = (int)Math.Ceiling((double)source.Length / (double)count);

            for (var i = 0; i < length; i++)
            {
                int start = count * i;

                if (source.Length <= start)
                {
                    yield break;
                }

                if (source.Length < start + count)
                {
                    yield return source.Substring(start);
                }
                else
                {
                    yield return source.Substring(start, count);
                }
            }
        }

        public static string Concat(this IEnumerable<string> strs)
        {
            if ((object)strs == null)
            {
                throw new ArgumentNullException("strs");
            }

            var builder = new StringBuilder();

            foreach (var str in strs)
            {
                builder.Append(str);
            }

            return builder.ToString();
        }

        /// <summary>
        /// 文字列の配列を、与えられた文字列を区切り文字として、結合します。
        /// </summary>
        /// <param name="strs">結合する文字列の配列</param>
        /// <param name="separator">区切り文字</param>
        /// <returns>結合された文字列</returns>
        /// <exception cref="System.ArgumentNullException">引数が null の場合</exception>
        public static string Concat(this string[] strs, string separator)
        {
            if ((object)strs == null)
            {
                throw new ArgumentNullException("strs");
            }

            if ((object)separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            var i = -1;
            var builder = new StringBuilder();

            foreach (var str in strs)
            {
                checked { i++; }

                if (0 < i)
                {
                    builder.Append(separator);
                }

                builder.Append(str);
            }

            return builder.ToString();
        }

        public static string Concat(this IEnumerable<string> strs, string separator)
        {
            if ((object)strs == null)
            {
                throw new ArgumentNullException("strs");
            }

            if ((object)separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            var i = -1;
            var builder = new StringBuilder();

            foreach (var str in strs)
            {
                checked { i++; }

                if (0 < i)
                {
                    builder.Append(separator);
                }

                builder.Append(str);
            }

            return builder.ToString();
        }

        public static string Concat(this IEnumerable<char> chars, char separator)
        {
            var count = chars.Count();
            if (count < 1)
            {
                return string.Empty;
            }

            var result = new List<char>(count * 2 - 1);
            result.Add(chars.First());

            foreach (var c in chars.Skip(1))
            {
                result.Add(separator);
                result.Add(c);
            }


            return new string(result.ToArray());
        }

        public static string Concat(this IEnumerable<char> chars)
        {
            return new string(chars.ToArray());
        }

        public static string Concat(this IEnumerable<char> chars, string separator)
        {
            if ((object)chars == null)
            {
                throw new ArgumentNullException("chars");
            }

            if ((object)separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            var i = -1;
            var builder = new StringBuilder();

            foreach (var c in chars)
            {
                checked { i++; }

                if (0 < i)
                {
                    builder.Append(separator);
                }

                builder.Append(c);
            }

            return builder.ToString();
        }


        /// <summary>
        /// 文字列を行ごとに区切ります
        /// </summary>
        /// <param name="text">区切られる文字列</param>
        /// <returns>各行ごとの文字列の配列</returns>
        /// <exception cref="System.ArgumentNullException">引数が null の場合</exception>
        public static string[] SplitLines(this string text)
        {
            if ((object)text == null)
            {
                throw new ArgumentNullException("text");
            }

            return text.Split('\n', '\r');
        }

        public static string Intercalate(this IEnumerable<string> source, string separator)
        {
            var i = -1;
            var builder = new StringBuilder();

            foreach (var elem in source)
            {
                checked { i++; }

                if (0 < i)
                {
                    builder.Append(separator);
                }

                builder.Append(elem);
            }

            return builder.ToString();
        }
    }
}
