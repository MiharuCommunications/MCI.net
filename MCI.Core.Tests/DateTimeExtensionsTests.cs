namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

                    Assert.True(max - min <= span);
                }
            }
        }



        [Theory,
        InlineData("2015/08/10 10:31:30", "2015/08/09 10:31:30"),
        InlineData("2015/09/01 10:31:30", "2015/08/31 10:31:30"),
        InlineData("2015/03/01 10:31:30", "2015/02/28 10:31:30"),
        InlineData("2016/03/01 10:31:30", "2016/02/29 10:31:30"),
        InlineData("2016/02/29 10:31:30", "2016/02/28 10:31:30"),
        ]
        public void GetTomorrowTest(string expected, string from)
        {
            var e = DateTime.Parse(expected);
            var f = DateTime.Parse(from);

            Assert.True(e.IsSameSecond(f.GetTomorrow()));
        }


        [Theory,
        InlineData("2015/08/10 10:31:30", "2015/08/11 10:31:30"),
        InlineData("2015/08/31 10:31:30", "2015/09/01 10:31:30"),
        InlineData("2015/02/28 10:31:30", "2015/03/01 10:31:30"),
        InlineData("2016/02/29 10:31:30", "2016/03/01 10:31:30"),
        InlineData("2016/02/28 10:31:30", "2016/02/29 10:31:30"),
        ]
        public void GetYesterdayTest(string expected, string from)
        {
            var e = DateTime.Parse(expected);
            var f = DateTime.Parse(from);

            Assert.True(e.IsSameSecond(f.GetYesterday()));
        }



        public static IEnumerable<object[]> GetEnumerateDaysSource()
        {
            yield return new object[]
            {
                new DateTime[] { new DateTime(2016, 3, 25), new DateTime(2016, 3, 26), new DateTime(2016, 3, 27) },
                new DateTime(2016, 3, 25, 12, 11, 10), new DateTime(2016, 3, 27, 11, 10, 10)
            };
        }

        [Theory, MemberData(nameof(GetEnumerateDaysSource))]
        public void EnumerateDays(DateTime[] expected, DateTime start, DateTime end)
        {
            var result = start.EnumerateDays(end);

            Assert.True(expected.Length == result.Count());

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.True(expected[i].IsSameDate(result.ElementAt(i)));
            }
        }


        public static IEnumerable<object[]> GetEnumerateMonthsSource()
        {
            yield return new object[]
            {
                new DateTime[] { new DateTime(2016, 3, 1), new DateTime(2016, 4, 1), new DateTime(2016, 5, 1) },
                new DateTime(2016, 3, 25, 12, 11, 10), new DateTime(2016, 5, 27, 11, 10, 10)
            };
        }

        [Theory, MemberData(nameof(GetEnumerateMonthsSource))]
        public void EnumerateMonths(DateTime[] expected, DateTime start, DateTime end)
        {
            var result = start.EnumerateMonths(end);

            Assert.True(expected.Length == result.Count());

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.True(expected[i].IsSameDate(result.ElementAt(i)));
            }
        }





        public static IEnumerable<object[]> GetNextEachMinuteTestSource()
        {
            yield return new object[] { "2016/5/1 0:1:0", "2016/5/1 0:0:1", 0 };
            yield return new object[] { "2016/5/1 0:0:0", "2016/5/1 0:0:0", 0 };

            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:20", 30 };
            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:30", 30 };
            yield return new object[] { "2015/8/10 10:31:30", "2015/8/10 10:30:40", 30 };
        }

        [Theory,
        MemberData(nameof(GetNextEachMinuteTestSource))]
        public void NextEachMinuteTest(string expected, string source, int second)
        {
            var e = DateTime.Parse(expected);
            var s = DateTime.Parse(source);

            Assert.Equal(e.Second, second);

            Assert.True(e.IsSameSecond(s.NextEachMinute(second)));
        }



        public static IEnumerable<object[]> GetNextEachHourTestSource()
        {
            yield return new object[] { "2016/5/1 1:0:0", "2016/5/1 0:1:1", 0, 0 };
            yield return new object[] { "2016/5/1 0:0:0", "2016/5/1 0:0:0", 0, 0 };

            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:20", 30, 30 };
            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:30", 30, 30 };
            yield return new object[] { "2015/8/10 11:30:30", "2015/8/10 10:30:40", 30, 30 };
        }

        [Theory,
        MemberData(nameof(GetNextEachHourTestSource))]
        public void NextEachHourTest(string expected, string source, int minute, int second)
        {
            var e = DateTime.Parse(expected);
            var s = DateTime.Parse(source);

            Assert.Equal(e.Second, second);

            Assert.True(e.IsSameSecond(s.NextEachHour(minute, second)));
        }




        public static IEnumerable<object[]> GetNextEachDaySource()
        {
            yield return new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 20), 10, 30, 30 };
            yield return new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 30), 10, 30, 30 };
            yield return new object[] { new DateTime(2015, 8, 11, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 40), 10, 30, 30 };
        }

        [Theory, MemberData(nameof(GetNextEachDaySource))]
        public void NextEachDay(DateTime expected, DateTime from, int hour, int minute, int second)
        {
            Assert.True(expected.IsSameSecond(from.NextEachDay(hour, minute, second)));
        }
    }
}
