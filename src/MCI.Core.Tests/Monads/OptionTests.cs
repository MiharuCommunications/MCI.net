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
        [Fact]
        public void SomeTests()
        {
            var opt = Option<int>.Return(0);

            // Propertiese
            Assert.True(opt.IsDefined);
            Assert.False(opt.IsEmpty);

            // Get
            Assert.Equal(0, opt.Get());

            Assert.Equal(0, opt.GetOrElse(100));
            Assert.Equal(0, opt.GetOrElse(() =>
            {
                throw new Exception();
            }));

            // ForEach
            Assert.Throws<InvalidOperationException>(() =>
            {
                opt.ForEach(i =>
                {
                    throw new InvalidOperationException();
                });
            });

            // Select
            Assert.Equal(1, opt.Select(i => i + 1).Get());
            Assert.Equal(1, opt.SelectMany(i => Option.Return(i + 1)).Get());

            // Recover
            Assert.True(opt.Recover(() =>
            {
                throw new InvalidOperationException();
            }).IsDefined);

            // Where
            Assert.Equal(0, opt.Where(i => true).Get());
            Assert.True(opt.Where(i => false).IsEmpty);

            // ToTry
            Assert.Equal(0, opt.ToTry().Get());
            Assert.Equal(0, opt.ToTry(new InvalidOperationException()).Get());
        }

        [Fact]
        public void NoneTests()
        {
            var opt = Option<int>.Fail();

            // Propertiese
            Assert.False(opt.IsDefined);
            Assert.True(opt.IsEmpty);

            // Get
            Assert.Throws<NullReferenceException>(() =>
            {
                opt.Get();
            });

            Assert.Equal(0, opt.GetOrElse(0));
            Assert.Equal(0, opt.GetOrElse(() =>
            {
                return 0;
            }));

            // ForEach
            opt.ForEach(i =>
            {
                Assert.True(false);
            });

            // Select
            Assert.True(opt.Select(i => i + 1).IsEmpty);
            Assert.True(opt.SelectMany(i => Option.Return(i + 1)).IsEmpty);

            // Recover
            Assert.Equal(1, opt.Recover(() => 1).Get());

            // Where
            Assert.True(opt.Where(i => true).IsEmpty);
            Assert.True(opt.Where(i => false).IsEmpty);

            // ToTry
            Assert.Throws<NullReferenceException>(() =>
            {
                opt.ToTry().Get();
            });
            Assert.Throws<InvalidOperationException>(() =>
            {
                opt.ToTry(new InvalidOperationException()).Get();
            });
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
            try
            {
                return Option<int>.Return(int.Parse(str));
            }
            catch
            {
                return Option<int>.Fail();
            }
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
