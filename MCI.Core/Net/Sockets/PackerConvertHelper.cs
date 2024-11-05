namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class PackerConvertHelper
    {
        public static string ConvertToString(byte[] bytes, Encoding encoding)
        {
            try
            {
                return encoding.GetString(bytes).Trim().Trim(new char[] { '\0' });
            }
            catch
            {
                return string.Empty;
            }
        }


        public static byte[] ConvertToPacket(string text, Encoding encoding)
        {
            try
            {
                return encoding.GetBytes(text);
            }
            catch
            {
                return new byte[0] { };
            }
        }

    }
}
