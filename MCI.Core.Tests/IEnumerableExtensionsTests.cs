namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void IndexOf()
        {
            IEnumerable<int> ls = new List<int> { 0, 1, 2, 3, 4 };

            Assert.True(1 == ls.IndexOf(n => n == 1));
            Assert.True(-1 == ls.IndexOf(n => n == 5));
        }



        [Fact]
        public void Fold()
        {
            IEnumerable<int> ls = Enumerable.Range(1, 5);

            Assert.True(15 == ls.FoldLeft(0, (sum, n) => sum + n));
            Assert.True(15 == ls.FoldRight((sum, n) => sum + n, 0));

            Assert.True("12345" == ls.FoldLeft(string.Empty, (sum, n) => sum + n.ToString()));
            Assert.True("54321" == ls.FoldRight((sum, n) => sum + n.ToString(), string.Empty));
        }



        [Theory,
        InlineData(new int[] { }, new int[] { }),
        InlineData(new int[] { }, new int[] { 1 }),
        InlineData(new int[] { 1 }, new int[] { 1, 2 }),
        InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4 })]
        public void InitTest(int[] expected, int[] source)
        {
            Assert.Equal(expected, source.Init());
        }



        [Theory,
        InlineData(new int[] { }, new int[] { }),
        InlineData(new int[] { }, new int[] { 1 }),
        InlineData(new int[] { 2, 3, 4 }, new int[] { 1, 2, 3, 4, })]
        public void TailTest(int[] expected, int[] source)
        {
            Assert.Equal(expected, source.Tail());
        }



        public static IEnumerable<object[]> TailsTestSource()
        {
            yield return new object[] { new int[][] { }, new int[] { 1 } };
            yield return new object[] { new int[][] { new int[] { 2, 3, 4 }, new int[] { 3, 4 }, new int[] { 4 } }, new int[] { 1, 2, 3, 4 } };
        }


        [Theory, MemberData(nameof(TailsTestSource))]
        public void TailsTest(int[][] expected, int[] source)
        {
            Assert.Equal(expected, source.Tails().Select(c => c.ToArray()).ToArray());
        }


        [Theory,
        InlineData(new int[] { }, new int[] { }, 0),
        InlineData(new int[] { }, new int[] { }, 3),
        InlineData(new int[] { }, new int[] { 1 }, 1),
        InlineData(new int[] { }, new int[] { 1 }, 3),
        InlineData(new int[] { 4, 5, 6 }, new int[] { 1, 2, 3, 4, 5, 6 }, 3)]
        public void DropTest(int[] expected, int[] source, int drop)
        {
            Assert.Equal(expected, source.Drop(drop));
        }



        [Theory,
        InlineData(new int[] { }, new int[] { }, 0),
        InlineData(new int[] { }, new int[] { }, 3),
        InlineData(new int[] { }, new int[] { 1 }, 1),
        InlineData(new int[] { }, new int[] { 1 }, 3),
        InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, 0),
        InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4, 5, 6 }, 3)]
        public void DropRightTest(int[] expected, int[] source, int drop)
        {
            Assert.Equal(expected, source.DropRight(drop));
        }







        [Fact]
        public void IsSame()
        {
            Assert.True(new List<int>() { 1, 2, 3 }.IsSame(new List<int>() { 1, 2, 3 }));
            Assert.True(new List<string>() { "1", "2", "3" }.IsSame(new List<string>() { "1", "2", "3" }));
        }



        public static IEnumerable<object[]> MargeDecimalSource()
        {
            yield return new object[] { null, new decimal[] { } };
            yield return new object[] { null, new decimal[] { 1.0M, 2.0M } };
            yield return new object[] { 1.0M, new decimal[] { 1.0M, 1.0M } };
        }

        [Theory, MemberData(nameof(MargeDecimalSource))]
        public void MargeDecimalTest(decimal? expected, decimal[] source)
        {
            Assert.Equal(expected, source.Marge());
        }



        public static IEnumerable<object[]> MargeIntSource()
        {
            yield return new object[] { null, new int[] { } };
            yield return new object[] { null, new int[] { 1, 2 } };
            yield return new object[] { 1, new int[] { 1, 1 } };
        }

        [Theory, MemberData(nameof(MargeIntSource))]
        public void MargeIntTest(int? expected, int[] source)
        {
            Assert.Equal(expected, source.Marge());
        }





        public static readonly Func<Tuple<int, DateTime>, DateTime> DateTimePicker = t => t.Item2;


        public static readonly object[] GroupedWithTimeSpanSource =
        {
            new object[]
            {
                new Tuple<int, DateTime>[]
                {
                    new Tuple<int, DateTime>(0, new DateTime(2015, 7, 1, 10, 30, 0)),
                    new Tuple<int, DateTime>(0, new DateTime(2015, 7, 1, 10, 0, 0)),
                    new Tuple<int, DateTime>(0, new DateTime(2015, 7, 1, 10, 5, 0)),
                    new Tuple<int, DateTime>(0, new DateTime(2015, 7, 1, 10, 10, 0)),
                    new Tuple<int, DateTime>(0, new DateTime(2015, 7, 1, 10, 20, 0)),
                    new Tuple<int, DateTime>(0, new DateTime(2015, 7, 1, 10, 15, 0)),
                    new Tuple<int, DateTime>(0, new DateTime(2015, 7, 1, 10, 25, 0)),
                },
                DateTimePicker,
                new DateTime(2015, 7, 1, 10, 00, 00),
                TimeSpan.FromMinutes(5)
            }
        };

    }
}
