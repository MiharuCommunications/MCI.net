namespace Miharu.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class TryTests
    {
        [Fact]
        public void SuccessTests()
        {
            var ex = new InvalidOperationException();

            var success = Try.Success();

            Assert.True(success.IsSuccess);
            Assert.False(success.IsFailure);
        }


        [Fact]
        public void FailureTests()
        {
            var ex = new InvalidOperationException();

            var failure = Try.Fail(ex);

            Assert.False(failure.IsSuccess);
            Assert.True(failure.IsFailure);
        }


        [Fact]
        public void SuccessVTests()
        {
            var value = 0;
            var ex = new InvalidOperationException();

            var success = Try<int>.Success(value);

            Assert.True(success.IsSuccess);
            Assert.False(success.IsFailure);

            Assert.Equal(value, success.Get());
            Assert.Equal(value, success.GetOrElse(value + 1));
            Assert.Equal(value, success.GetOrElse(() => value + 1));
        }


        [Fact]
        public void FailureVTests()
        {
            var value = 0;
            var ex = new InvalidOperationException();

            var failure = Try<int>.Fail(ex);

            Assert.False(failure.IsSuccess);
            Assert.True(failure.IsFailure);

            Assert.Throws<InvalidOperationException>(() =>
            {
                failure.Get();
            });
            Assert.Equal(value, failure.GetOrElse(value));
            Assert.Equal(value, failure.GetOrElse(() => value));
        }







        [Fact]
        public void LINQTest()
        {
            var result = from n1 in GetInt("1")
                         from n2 in GetInt("2")
                         select n1 + n2;

            Assert.True(result.IsSuccess);
            Assert.True(result.GetOrElse(0) == 3);
        }

        [Fact]
        public void StackTraceTest()
        {
            var result = GetInt("zzz");

            return;
        }


        public static Try<int> GetInt(string str)
        {
            try
            {
                return Try<int>.Success(int.Parse(str));
            }
            catch (Exception ex)
            {
                return Try<int>.Fail(ex);
            }
        }
    }
}
