//-----------------------------------------------------------------------
// <copyright file="FutureExtensions.cs" company="Miharu Communications Inc.">
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

    public static class FutureExtensions
    {
        public static FutureAwaiter<A> GetAwaiter<A>(this Future<A> future)
        {
            return new FutureAwaiter<A>(future);
        }

        public static FutureAwaiter GetAwaiter(this Future future)
        {
            return new FutureAwaiter(future);
        }

        /*
         * 欲しい機能
         *
         *
         *
         *
         *
         *
         *
         *
         *
        **/

        public static Future ForEachInOrderToFailure<A>(this IEnumerable<A> collection, Func<A, Future> f)
        {


            throw new NotImplementedException();
        }


        public static Future ForEach<A>(this IEnumerable<A> collection, Func<A, Future> f)
        {
            throw new NotImplementedException();
        }


        public static Future ForEachToFailure<A>(this IEnumerable<A> collection, Func<A, Future> f)
        {
            throw new NotImplementedException();
        }
    }
}
