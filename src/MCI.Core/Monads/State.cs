//-----------------------------------------------------------------------
// <copyright file="State.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class State<S, A>
    {
        private readonly Func<S, Tuple<A, S>> runState;

        public State(Func<S, Tuple<A, S>> x)
        {
            this.runState = x;
        }



        public State<S, B> FlatMap<B>(Func<A, State<S, B>> f)
        {
            return new State<S, B>(s =>
            {
                var result = this.runState(s);

                return f(result.Item1).runState(result.Item2);
            });
        }

        public State<S, B> Map<B>(Func<A, B> f)
        {
            return new State<S, B>(s =>
            {
                var result = this.runState(s);

                return State.Return<S, B>(f(result.Item1)).runState(result.Item2);
            });
        }



    }


    public class State
    {
        public static State<S, A> Return<S, A>(A a)
        {
            return new State<S, A>(s => new Tuple<A, S>(a, s));
        }
    }
}
