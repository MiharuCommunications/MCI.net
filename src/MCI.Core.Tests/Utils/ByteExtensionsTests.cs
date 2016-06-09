namespace Miharu.Core.Tests.Utils
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
        public static object[] ToByteArraySuccessSource = new object[]
        {
            new object[] { new byte[] { }, "" },
            new object[] { new byte[] { 0x00 }, "00" },
            new object[] { new byte[] { 0xFF }, "FF" },
            new object[] { new byte[] { 0xFF, 0xFF }, "FFFF" },
        };


        [Theory, MemberData("ToByteArraySuccessSource")]
        public void ToByteArraySuccess(byte[] expected, string source)
        {
            var result = source.ToByteArray();

            Assert.True(result.IsSuccess);
            Assert.Equal(expected, result.Get());
        }


        public static object[] ToByteArrayFailureSource = new object[]
        {
            new object[] { new NullReferenceException(), null },
            new object[] { new FormatException(), "F" },
            new object[] { new FormatException(), "GG" },
        };


        [Theory, MemberData("ToByteArrayFailureSource")]
        public void ToByteArrayFailure(Exception expected, string source)
        {
            var result = source.ToByteArray();

            Assert.True(result.IsFailure);
            Assert.Equal(expected.GetType(), result.GetException().GetType());
        }




        public static readonly object[] ToHexStringSource = new object[]
        {
            new object[] { "", new byte[] { } },
            new object[] { "FF", new byte[] { 0xFF } },
        };


        [Theory, MemberData("ToHexStringSource")]
        public void ToHexString(string expected, byte[] source)
        {
            Assert.Equal(expected, source.ToHexString());
        }
    }
}
