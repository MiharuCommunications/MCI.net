using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miharu.Core.Tests.Extensions
{
    public class DateTimeExtensionsTests
    {
        public static readonly object[] IsSameDateSource =
        {
            new object[] { true, new DateTime(2015, 7, 13), new DateTime(2015, 7, 13) },
            new object[] { false, new DateTime(2015, 7,14), new DateTime(2015, 7, 13) },
        };



        [Theory, MemberData("IsSameDateSource")]
        public void IsSameDateTest(bool expected, DateTime dt1, DateTime dt2)
        {
            Assert.Equal(expected, dt1.IsSameDate(dt2));
        }




        public static readonly object[] ToSource =
        {
            new object[] { new DateTime[] { new DateTime(2015, 7, 13) }, new DateTime(2015, 7, 13), new DateTime(2015, 7, 13) },

            new object[] { new DateTime[] { new DateTime(2015, 7, 13), new DateTime(2015, 7, 14) }, new DateTime(2015, 7, 13), new DateTime(2015, 7, 14) },
            new object[] { new DateTime[] { new DateTime(2015, 7, 14), new DateTime(2015, 7, 13) }, new DateTime(2015, 7, 14), new DateTime(2015, 7, 13) },
        };



        [Theory, MemberData("ToSource")]
        public void ToTest(DateTime[] expected, DateTime from, DateTime to)
        {
            Assert.Equal(expected, from.To(to));
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
                new DateTime(2015, 7, 1, 10, 0, 0),
                TimeSpan.FromMinutes(5)
            },

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
                new DateTime(2015, 7, 1, 0, 0, 0),
                TimeSpan.FromMinutes(15)
            },
        };


        [Theory, MemberData("GroupedWithTimeSpanSource")]
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



        public static object[] NextEachMinuteSource
        {
            get
            {
                return new object[]
                {
                    new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 20), 30 },
                    new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 30), 30 },
                    new object[] { new DateTime(2015, 8, 10, 10, 31, 30), new DateTime(2015, 8, 10, 10, 30, 40), 30 },
                };
            }
        }

        [Theory, MemberData("NextEachMinuteSource")]
        public void NextEachMinute(DateTime expected, DateTime from, int second)
        {
            Assert.True(expected.IsSameSecond(from.NextEachMinute(second)));
        }




        public static object[] NextEachHourSource
        {
            get
            {
                return new object[]
                {
                    new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 20), 30, 30 },
                    new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 30), 30, 30 },
                    new object[] { new DateTime(2015, 8, 10, 11, 30, 30), new DateTime(2015, 8, 10, 10, 30, 40), 30, 30 },
                };
            }
        }

        [Theory, MemberData("NextEachHourSource")]
        public void NextEachHour(DateTime expected, DateTime from, int minute, int second)
        {
            Assert.True(expected.IsSameSecond(from.NextEachHour(minute, second)));
        }




        public static object[] NextEachDaySource
        {
            get
            {
                return new object[]
                {
                    new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 20), 10, 30, 30 },
                    new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 30), 10, 30, 30 },
                    new object[] { new DateTime(2015, 8, 11, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 40), 10, 30, 30 },
                };
            }
        }

        [Theory, MemberData("NextEachDaySource")]
        public void NextEachDay(DateTime expected, DateTime from, int hour, int minute, int second)
        {
            Assert.True(expected.IsSameSecond(from.NextEachDay(hour, minute, second)));
        }




    }
}
