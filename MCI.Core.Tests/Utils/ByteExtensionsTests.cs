namespace Miharu.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;
    using Miharu.Utils;


    public class ByteExtensionsTests
    {
        public static IEnumerable<object[]> GetToByteArraySourceSuccessCases()
        {
            yield return new object[] { new byte[] { }, "" };
            yield return new object[] { new byte[] { 0x00 }, "00" };
            yield return new object[] { new byte[] { 0xFF }, "FF" };
            yield return new object[] { new byte[] { 0xFF, 0xFF }, "FFFF" };
        }

        [Theory, MemberData(nameof(GetToByteArraySourceSuccessCases))]
        public void ToByteArraySuccessCase(byte[] expected, string source)
        {
            var result = source.ToByteArray();

            Assert.Equal(expected, result);
        }


        [Fact]
        public void ToByteArrayFailureCases()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ((string)null).ToByteArray();
            });

            Assert.Throws<FormatException>(() =>
            {
                "F".ToByteArray();
            });

            Assert.Throws<FormatException>(() =>
            {
                "GG".ToByteArray();
            });
        }




        public static IEnumerable<object[]> GetToHexStringSourceSuccessCases()
        {
            yield return new object[] { "", new byte[] { } };
            yield return new object[] { "FF", new byte[] { 0xFF } };
        }


        [Theory, MemberData(nameof(GetToHexStringSourceSuccessCases))]
        public void ToHexString(string expected, byte[] source)
        {
            Assert.Equal(expected, source.ToHexString());
        }
    }
}
