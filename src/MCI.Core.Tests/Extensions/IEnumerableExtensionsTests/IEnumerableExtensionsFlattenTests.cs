using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Miharu.Core.Tests.Extensions.IEnumerableExtensionsTests
{
    public class IEnumerableExtensionsFlattenTests
    {
        public static IEnumerable<object[]> GetFlatten2TestSource()
        {
            yield return new object[]
            {
                new int[] { },
                new int[][] { new int[] { }, new int[] { } }
            };

            yield return new object[]
            {
                new int[] { 1, 2, 1, 2 },
                new int[][] { new int[] { 1, 2 }, new int[] { 1, 2 } }
            };
        }

        [Theory, MemberData("GetFlatten2TestSource")]
        public void Flatten2Test(int[] expected, int[][] source)
        {
            Assert.Equal(expected, source.Flatten().ToArray());
        }
    }
}
