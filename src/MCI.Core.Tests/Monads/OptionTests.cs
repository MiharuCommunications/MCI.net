using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miharu.Core.Tests.Monads
{
    public class OptionTests
    {
        private readonly Option<int> some = Option.Return(0);
        private readonly Option<int> none = Option.Fail<int>();

        private readonly Func<int, int> sq = i => i * i;

        public void IsSome<T>(Option<T> opt, T value)
        {
            Assert.True(opt.IsDefined);

            Assert.Equal(opt.Get(), value);
        }

        public void IsNone<T>(Option<T> opt)
        {
            Assert.True(opt.IsEmpty);
        }

        [Fact]
        public void CreateTest()
        {
            Assert.True(this.some.IsDefined);

            Assert.False(this.none.IsDefined);
        }


        [Fact]
        public void IsTest()
        {
            Assert.True(this.some.IsDefined);
            Assert.False(this.some.IsEmpty);

            Assert.False(this.none.IsDefined);
            Assert.True(this.none.IsEmpty);
        }


        [Fact]
        public void SelectTest()
        {
            var some2 = this.some.Select(this.sq);
            var none2 = this.none.Select(this.sq);

            this.IsSome(some2, this.sq(this.some.Get()));

            this.IsNone(none2);
        }


        [Fact]
        public void SelectManyTest()
        {
            var some2 = this.some.SelectMany(i => Option.Return(this.sq(i)));
            var none2 = this.some.SelectMany(i => Option.Fail<int>());

            var none3 = this.none.SelectMany(i => Option.Return(0));

            this.IsSome<int>(some2, this.sq(this.some.Get()));

            this.IsNone(none2);
            this.IsNone(none3);
        }



        [Fact]
        public void LINQTest()
        {
            var result = from x1 in GetIntOpt("1")
                         from x2 in GetIntOpt("2")
                         from x3 in GetIntOpt("3")
                         from x4 in GetIntOpt("4")
                         from x5 in GetIntOpt("5")
                         from x6 in GetIntOpt("6")
                         from x7 in GetIntOpt("7")
                         from x8 in GetIntOpt("8")
                         from x9 in GetIntOpt("9")
                         from x10 in GetIntOpt("10")
                         from x11 in GetIntOpt("11")
                         from x12 in GetIntOpt("12")
                         from x13 in GetIntOpt("13")
                         from x14 in GetIntOpt("14")
                         from x15 in GetIntOpt("15")
                         from x16 in GetIntOpt("16")
                         from x17 in GetIntOpt("17")
                         from x18 in GetIntOpt("18")
                         from x19 in GetIntOpt("19")
                         from x20 in GetIntOpt("20")
                         from x21 in GetIntOpt("21")
                         from x22 in GetIntOpt("22")
                         from x23 in GetIntOpt("23")
                         from x24 in GetIntOpt("24")
                         from x25 in GetIntOpt("25")
                         from x26 in GetIntOpt("26")
                         from x27 in GetIntOpt("27")
                         from x28 in GetIntOpt("28")
                         from x29 in GetIntOpt("29")
                         from x30 in GetIntOpt("30")
                         select x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9 + x10 + x11 + x12 + x13 + x14 + x15 + x16 + x17 + x18 + x19 + x20 + x21 + x22 + x23 + x24 + x25 + x26 + x27 + x28 + x29 + x30;

            Assert.True(result.IsDefined);
            Assert.True(result.Select(num => num == 465).GetOrElse(false));

            var r = from x1 in GetIntOpt("1")
                    from x2 in GetIntOpt("2")
                    from x3 in AddIntOpt(x1, x2)
                    select x1 + x2 + x3;
        }


        public static Option<int> GetIntOpt(string str)
        {
            return Try<int>.Execute(() => int.Parse(str)).ToOption();
        }

        public static Option<int> AddIntOpt(int x, int y)
        {
            var z = x + y;

            if (z < 2)
            {
                return Option<int>.Return(z);
            }
            else
            {
                return Option<int>.Fail();
            }
        }


    }
}
