namespace Miharu.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class DateTimeExtensionsTests
    {
        public static IEnumerable<object[]> GetIsSameDateSource()
        {
            yield return new object[] { true, new DateTime(2015, 7, 13), new DateTime(2015, 7, 13) };
            yield return new object[] { false, new DateTime(2015, 7, 14), new DateTime(2015, 7, 13) };
        }


        [Theory, MemberData(nameof(GetIsSameDateSource))]
        public void IsSameDateTest(bool expected, DateTime dt1, DateTime dt2)
        {
            Assert.Equal(expected, dt1.IsSameDate(dt2));
        }


        public static IEnumerable<object[]> GetToSource()
        {
            yield return new object[] { new DateTime[] { new DateTime(2015, 7, 13) }, new DateTime(2015, 7, 13), new DateTime(2015, 7, 13) };

            yield return new object[] { new DateTime[] { new DateTime(2015, 7, 13), new DateTime(2015, 7, 14) }, new DateTime(2015, 7, 13), new DateTime(2015, 7, 14) };
            yield return new object[] { new DateTime[] { new DateTime(2015, 7, 14), new DateTime(2015, 7, 13) }, new DateTime(2015, 7, 14), new DateTime(2015, 7, 13) };
        }


        [Theory, MemberData(nameof(GetToSource))]
        public void ToTest(DateTime[] expected, DateTime from, DateTime to)
        {
            Assert.Equal(expected, from.To(to));
        }





        public static readonly Func<Tuple<int, DateTime>, DateTime> DateTimePicker = t => t.Item2;


        public static IEnumerable<object[]> GroupedWithTimeSpanSource()
        {
            yield return new object[]
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
                new DateTime(2015, 7, 1, 10, 0, 0),
                TimeSpan.FromMinutes(5)
            };

            yield return new object[]
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
                new DateTime(2015, 7, 1, 0, 0, 0),
                TimeSpan.FromMinutes(15)
            };
        }


        [Theory, MemberData(nameof(GroupedWithTimeSpanSource))]
        public void GroupedByTimeSpanTest(IEnumerable<Tuple<int, DateTime>> collection, Func<Tuple<int, DateTime>, DateTime> mapper, DateTime start, TimeSpan span)
        {
            var results = collection.GroupedWithTimeSpan(mapper, start, span);

            Assert.Equal(collection.Count(), results.SelectMany(i => i).Count());

            foreach (var result in results)
            {
                var items = result.ToArray();

                if (items.Length < 2)
                {
                    continue;
                }

                for (var i = 0; i < items.Length - 1; i++)
                {
                    Assert.True(mapper(items[i]) <= mapper(items[i + 1]));
                }
            }

            foreach (var result in results)
            {
                var times = result.Select(mapper).ToArray();

                if (times.Length < 1)
                {
                    continue;
                }
                else
                {
                    var max = times.Max();
                    var min = times.Min();

                    Assert.True((max - min) <= span);
                }
            }
        }


    }
}
