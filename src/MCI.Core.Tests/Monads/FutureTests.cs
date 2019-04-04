namespace Miharu.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class FutureTests
    {

        public static readonly Func<int, int> Square = i => i * i;


        [Fact]
        public void LINQTest()
        {
            var result = from f1 in GetFutureInt("1")
                         from f2 in GetFutureInt("2")
                         select f1 + f2;

            Assert.Equal(3, result.Get());
        }


        [Fact]
        public void SelectTest()
        {
            var result = GetFutureInt("1").Select(Square);

            Assert.Equal(1, result.AsTask().Result.Get());

        }


        [Fact]
        public void SelectManyTest()
        {
            var result = GetFutureInt("1").SelectMany(i => GetFutureInt("2"));

            Assert.Equal(2, result.Get());
        }




        [Fact]
        public void FromExecuteTest()
        {
            var future = Future.FromExecute(() => 1);


            Assert.True(future.FutureTask.IsCompleted);
            Assert.Equal(1, future.FutureTask.Result.Get());
        }



        [Fact]
        public async Task AsyncTest()
        {
            var result = await GetFutureInt("1");
        }


        public static Future<int> GetFutureInt(string text)
        {
            return Future.FromExecute(() =>
            {
                return int.Parse(text);
            });
        }
    }
}
