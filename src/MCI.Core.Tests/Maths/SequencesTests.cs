namespace Miharu.Maths
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Miharu.Maths;
    using Xunit;

    public class SequencesTests
    {
        [Fact]
        public void Pow2s()
        {
            var max = Sequences.Pow2s.Length - 1;

            for (var i = 0; i < max; i++)
            {
                Assert.True(Sequences.Pow2s[i] * 2 == Sequences.Pow2s[i + 1]);
            }
        }


        [Fact]
        public void Pow10s()
        {
            var max = Sequences.Pow10s.Length - 1;

            for (var i = 0; i < max; i++)
            {
                Assert.True(Sequences.Pow10s[i] * 10 == Sequences.Pow10s[i + 1]);
            }
        }



        public static IEnumerable<object[]> Pow10Source()
        {
            yield return new object[] { 1E10M, 10 };
            yield return new object[] { 1E5M, 5 };
            yield return new object[] { 1M, 0 };
            yield return new object[] { 1E-5M, -5 };
            yield return new object[] { 1E-10M, -10 };
        }


        [Theory, MemberData(nameof(Pow10Source))]
        public void Pow10(decimal expected, int index)
        {
            Assert.Equal(expected, Sequences.Pow10(index));
        }
    }
}
