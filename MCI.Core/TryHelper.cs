//-----------------------------------------------------------------------
// <copyright file="TryHelper.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
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

        public static Try ReturnArgumentNullException(string paramName)
        {
            try
            {
                throw new ArgumentNullException(paramName);
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public static Try<T> ReturnArgumentNullException<T>(string paramName)
        {
            try
            {
                throw new ArgumentNullException(paramName);
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }


        public static Try ReturnArgumentOutOfRangeException(string paramName)
        {
            try
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public static Try<T> ReturnArgumentOutOfRangeException<T>(string paramName)
        {
            try
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }


        public static Try ReturnTimeoutException(string message)
        {
            try
            {
                throw new TimeoutException(message);
            }
            catch (Exception ex)
            {
                return Try.Fail(ex);
            }
        }

        public static Try<T> ReturnTimeoutException<T>(string message)
        {
            try
            {
                throw new TimeoutException(message);
            }
            catch (Exception ex)
            {
                return Try<T>.Fail(ex);
            }
        }

        public static Try ReturnNotImplementedException()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return Try.Fail(ex);
            }
        }

        public static Try<T> ReturnNotImplementedException<T>()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return Try<T>.Fail(ex);
            }
        }

        public static Try ReturnNotImplementedException(string message)
        {
            try
            {
                throw new NotImplementedException(message);
            }
            catch (Exception ex)
            {
                return Try.Fail(ex);
            }
        }

        public static Try<T> ReturnNotImplementedException<T>(string message)
        {
            try
            {
                throw new NotImplementedException(message);
            }
            catch (Exception ex)
            {
                return Try<T>.Fail(ex);
            }
        }



        public static Try ReturnObjectDisposedException(string message)
        {
            try
            {
                throw new ObjectDisposedException(message);
            }
            catch (Exception ex)
            {
                return Try.Fail(ex);
            }
        }

        public static Try<T> ReturnObjectDisposedException<T>(string message)
        {
            try
            {
                throw new ObjectDisposedException(message);
            }
            catch (Exception ex)
            {
                return Try<T>.Fail(ex);
            }
        }

    }
}
