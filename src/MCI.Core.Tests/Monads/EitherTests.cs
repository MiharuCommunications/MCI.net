namespace Miharu.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class EitherTests
    {
        private int rValue;

        private string lValue;

        private Either<string, int> r;

        private Either<string, int> l;

        public EitherTests()
        {
            this.rValue = 0;
            this.lValue = "0";

            this.r = new Right<string, int>(this.rValue);
            this.l = new Left<string, int>(this.lValue);
        }

        [Fact]
        public void IsTest()
        {
            Assert.True(this.r.IsRight);
            Assert.False(this.r.IsLeft);

            Assert.False(this.l.IsRight);
            Assert.True(this.l.IsLeft);
        }

        [Fact]
        public void GetTest()
        {
            Assert.Equal(this.rValue, this.r.Get());

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.l.Get();
            });
        }

        [Fact]
        public void GetOrElseTest()
        {
            Assert.Equal(this.rValue, this.r.GetOrElse(10));
            Assert.Equal(10, this.l.GetOrElse(10));

            Assert.Equal(this.rValue, this.r.GetOrElse(() =>
            {
                throw new InvalidOperationException();
            }));

            Assert.Equal(10, this.l.GetOrElse(() =>
            {
                return 10;
            }));
        }

        [Fact]
        public void OrElseTest()
        {
            Assert.Equal(this.rValue, this.r.OrElse(() =>
            {
                throw new InvalidOperationException();
            }).Get());

            Assert.Equal(this.rValue, this.l.OrElse(() =>
            {
                return this.r;
            }).Get());
        }

        [Fact]
        public void SwapTest()
        {
            Assert.True(this.r.Swap().IsLeft);
            Assert.True(this.l.Swap().IsRight);
        }

        [Fact]
        public void RecoverTest()
        {
            Assert.Equal(this.rValue, this.r.Recover(l =>
            {
                throw new InvalidOperationException();
            }).Get());

            Assert.Equal(10, this.l.Recover(l =>
            {
                return 10;
            }).Get());
        }

        [Fact]
        public void RecoverWithTest()
        {
            Assert.Equal(this.rValue, this.r.RecoverWith(l =>
            {
                throw new InvalidOperationException();
            }).Get());

            Assert.Equal(10, this.l.RecoverWith(l =>
            {
                return new Right<string, int>(10);
            }).Get());
        }

        [Fact]
        public void ExistsTest()
        {
            Assert.True(this.r.Exists(i => i == this.rValue));
            Assert.False(this.r.Exists(i => i != this.rValue));

            Assert.False(this.l.Exists(i => true));
        }

        [Fact]
        public void ForEachTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.r.ForEach(i =>
                {
                    throw new InvalidOperationException();
                });
            });

            this.l.ForEach(i =>
            {
                throw new InvalidOperationException();
            });
        }
    }
}
