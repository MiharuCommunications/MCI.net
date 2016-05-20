namespace Miharu.Core.Tests.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOptionTests<T>
    {
        Option<T> Value { get; }

        void IsEmptyTest();

        void IsDefinedTest();

        void MapTest();
        void FlatMapTest();

        void GetTest();
        void GetOrElseTest();

        void OrElseTest();

        void RecoverTest();

    }
}
