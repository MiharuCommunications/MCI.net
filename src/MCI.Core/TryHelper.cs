namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;

    /// <summary>
    /// <para>Try.Execute は StackTrace が引数で与えたラムダ式のところからとなってしまうので。</para>
    /// </summary>
    public static class TryHelper
    {
        public static Try ReturnArgumentOutOfRangeException()
        {
            try
            {
                throw new ArgumentOutOfRangeException();
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public static Try<T> ReturnArgumentOutOfRangeException<T>()
        {
            try
            {
                throw new ArgumentOutOfRangeException();
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }
    }
}
