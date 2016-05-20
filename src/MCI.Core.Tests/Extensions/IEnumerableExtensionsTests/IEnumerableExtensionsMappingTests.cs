using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Miharu.Core.Tests.Extensions.IEnumerableExtensionsTests
{
    public class IEnumerableExtensionsMappingTests
    {
        public static readonly Func<int, int> Square = i => i * i;

        public static readonly Func<int, IEnumerable<int>> Range = i => Enumerable.Range(1, i);

        public static readonly object[] MapTestSource =
        {
            new object[] { new int[] { }, new int[] { }, Square },
            new object[] { new int[] { 1 }, new int[] { 1 }, Square },
            new object[] { new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 4, 9, 16, 25 }, Square },
        };

        [Theory, MemberData("MapTestSource")]
        public void MapTest(IEnumerable<int> source, IEnumerable<int> dest, Func<int, int> f)
        {
            Assert.Equal(dest, source.Map(f));
        }



        public static readonly object[] FlatMapTestSource =
        {
            new object[] { new int[] { 1, 1, 2, 1, 2, 3 }, new int[] { 1, 2, 3 }, Range },
        };



        [Theory, MemberData("FlatMapTestSource")]
        public void FlatMapTest(IEnumerable<int> expected, IEnumerable<int> source, Func<int, IEnumerable<int>> f)
        {
            Assert.Equal(expected, source.FlatMap(f));
        }
    }
}
