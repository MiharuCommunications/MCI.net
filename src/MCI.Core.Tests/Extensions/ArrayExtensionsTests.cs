using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miharu.Core.Tests.Extensions
{
    public class ArrayExtensionsTests
    {
        [Fact]
        public void Divide()
        {
            var result = (new int[5] { 1, 2, 3, 4, 5 }).Divide(2);

            Assert.True(result[0].Length == 2);
            Assert.True(result[1].Length == 2);
            Assert.True(result[2].Length == 1);

            Assert.True(result[2][0] == 5);


            result = (new int[4] { 1, 2, 3, 4 }).Divide(2);

            Assert.True(result[0].Length == 2);
            Assert.True(result[1].Length == 2);

            Assert.True(result[1][1] == 4);
        }


        [Fact]
        public void IsSame()
        {
            Assert.True((new int[] { 1, 2, 3, 4, 5 }).IsSame(new int[] { 1, 2, 3, 4, 5 }));



            Assert.False((new int[] { 1, 2, 3, 4, 5 }).IsSame(new int[] { 1, 2, 3, 4 }));
        }
    }
}
