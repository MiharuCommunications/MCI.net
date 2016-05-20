using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu
{
    public struct Option2<T>
    {
        internal readonly T Value;
        internal readonly bool HasValue;


        internal Option2(T value)
        {
            this.Value = value;
            this.HasValue = true;
        }


        /// <summary>
        /// 空かどうか
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !this.HasValue;
            }
        }

        /// <summary>
        /// 値が含まれるかどうか
        /// </summary>
        public bool IsDefined
        {
            get
            {
                return this.HasValue;
            }
        }


        public Option2<B> Map<B>(Func<T, B> f)
        {
            if (this.HasValue)
            {
                return new Option2<B>(f(this.Value));
            }
            else
            {
                return new Option2<B>();
            }
        }


        public Option2<B> FlatMap<B>(Func<T, Option2<B>> f)
        {
            if (this.HasValue)
            {
                return f(this.Value);
            }
            else
            {
                return new Option2<B>();
            }
        }

        /// <summary>
        /// <para>値を取り出します。</para>
        /// <para>値がない場合例外 NullReferenceException が発生します。</para>
        /// </summary>
        /// <exception cref="System.NullReferenceException">値がない場合</exception>
        /// <returns>格納されている値</returns>
        public T Get()
        {
            if (this.HasValue)
            {
                return this.Value;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// <para>格納された値を取り出します。</para>
        /// <para>格納された値がない場合は、引数で渡した値が返ります。</para>
        /// </summary>
        /// <param name="value">値がなかった場合のデフォルト値</param>
        /// <returns>格納されている値</returns>
        public T GetOrElse(T value)
        {
            return this.HasValue ? this.Value : value;
        }


        public T GetOrElse(Func<T> f)
        {
            return this.HasValue ? this.Value : f();
        }

        public Option2<T> OrElse(Func<Option2<T>> f)
        {
            if (this.HasValue)
            {
                return new Option2<T>(this.Value);
            }
            else
            {
                return f();
            }
        }

        public Option2<T> Recover(Func<T> f)
        {
            if (this.HasValue)
            {
                return new Option2<T>(this.Value);
            }
            else
            {
                return new Option2<T>(f());
            }
        }

    }


    public struct Option2
    {
        public static Option2<T> Return<T>(T value)
        {
            return new Option2<T>(value);
        }

        public static Option2<T> Fail<T>()
        {
            return new Option2<T>();
        }
    }
}
