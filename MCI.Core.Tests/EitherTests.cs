namespace Miharu
{
    using System;
    using Xunit;

    public class EitherTests
    {
        private int rValue;

        private string lValue;

        private Either<string, int> r;

        private Either<string, int> l;

        public EitherTests()
        {
            rValue = 0;
            lValue = "0";

            r = new Right<string, int>(rValue);
            l = new Left<string, int>(lValue);
        }

        [Fact]
        public void IsTest()
        {
            Assert.True(r.IsRight);
            Assert.False(r.IsLeft);

            Assert.False(l.IsRight);
            Assert.True(l.IsLeft);
        }

        [Fact]
        public void GetTest()
        {
            Assert.Equal(rValue, r.Get());

            Assert.Throws<InvalidOperationException>(() =>
            {
                l.Get();
            });
        }

        [Fact]
        public void GetOrElseTest()
        {
            Assert.Equal(rValue, r.GetOrElse(10));
            Assert.Equal(10, l.GetOrElse(10));

            Assert.Equal(rValue, r.GetOrElse(() =>
            {
                throw new InvalidOperationException();
            }));

            Assert.Equal(10, l.GetOrElse(() =>
            {
                return 10;
            }));
        }

        [Fact]
        public void OrElseTest()
        {
            Assert.Equal(rValue, r.OrElse(() =>
            {
                throw new InvalidOperationException();
            }).Get());

            Assert.Equal(rValue, l.OrElse(() =>
            {
                return r;
            }).Get());
        }

        [Fact]
        public void SwapTest()
        {
            Assert.True(r.Swap().IsLeft);
            Assert.True(l.Swap().IsRight);
        }

        [Fact]
        public void RecoverTest()
        {
            Assert.Equal(rValue, r.Recover(l =>
            {
                throw new InvalidOperationException();
            }).Get());

            Assert.Equal(10, l.Recover(l =>
            {
                return 10;
            }).Get());
        }

        [Fact]
        public void RecoverWithTest()
        {
            Assert.Equal(rValue, r.RecoverWith(l =>
            {
                throw new InvalidOperationException();
            }).Get());

            Assert.Equal(10, l.RecoverWith(l =>
            {
                return new Right<string, int>(10);
            }).Get());
        }

        [Fact]
        public void ExistsTest()
        {
            Assert.True(r.Exists(i => i == rValue));
            Assert.False(r.Exists(i => i != rValue));

            Assert.False(l.Exists(i => true));
        }

        [Fact]
        public void ForEachTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                r.ForEach(i =>
                {
                    throw new InvalidOperationException();
                });
            });

            l.ForEach(i =>
            {
                throw new InvalidOperationException();
            });
        }


        public Either<string, int> ParseInt(string input)
        {
            int i;

            if (int.TryParse(input, out i))
            {
                return new Right<string, int>(i);
            }
            else
            {
                return new Left<string, int>("Error");
            }
        }

        [Fact]
        public void LinqTest()
        {
            var result = from i1 in ParseInt("1")
                         from i2 in ParseInt("2")
                         from i3 in ParseInt("3")
                         from i4 in ParseInt("4")
                         from i5 in ParseInt("5")
                         select i1 + i2 + i3 + i4 + i5;

            Assert.True(result.IsRight);
            Assert.Equal(15, result.Get());
        }
    }
}
