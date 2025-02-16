namespace Miharu.Utils.Converters.UrlEncode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Miharu.Utils.Converters.UrlEncode;
    using Xunit;

    public class UrlEncoderTests
    {
        public static IEnumerable<object[]> EncodeDecodeSources()
        {
            yield return new object[] { "ウィキペディア", Encoding.UTF8, "%E3%82%A6%E3%82%A3%E3%82%AD%E3%83%9A%E3%83%87%E3%82%A3%E3%82%A2" };
        }


        [Theory, MemberData(nameof(EncodeDecodeSources))]
        public void EncodeTest(string source, Encoding code, string encoded)
        {
            Assert.Equal(encoded, UrlEncoder.Encode(source, code));

            Assert.Equal(source, UrlEncoder.Decode(encoded, code));
        }

        [Theory(Skip = "not implemented"),
        InlineData("this is a pen.")]
        public void Compare(string source)
        {
            var codes = new Encoding[]
            {
                Encoding.UTF8,
            };

            foreach (var code in codes)
            {
                var encoded = HttpUtility.UrlEncode(source, code);

                Assert.Equal(encoded, UrlEncoder.Encode(source, code));
                Assert.Equal(source, UrlEncoder.Decode(encoded, code));
            }
        }

    }
}
