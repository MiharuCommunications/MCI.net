namespace Miharu.Maths
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Sequences
    {
        public static readonly int[] Pow2s = new int[]
        {
            1,
            2,
            4,
            8,
            16,
            32,
            64,
            128,
            256,
            512,
            1024,
            2048,
            4096,
            8192,
            16384,
            32768,
            65536,
            131072,
            262144,
            524288,
            1048576,
            2097152,
            4194304,
            8388608,
            16777216,
            33554432,
            67108864,
            134217728,
            268435456,
            536870912,
            1073741824
        };


        public static readonly int[] Pow10s = new int[]
        {
            1,
            10,
            100,
            1000,
            10000,
            100000,
            1000000,
            10000000,
            100000000,
            1000000000,
        };

        public static decimal Pow10(int index)
        {
            var time = 0 <= index ? index : -index;
            var abs = 1.0M;

            for (var i = 0; i < time; i++)
            {
                abs *= 10.0M;
            }

            if (index < 0)
            {
                return 1.0M / abs;
            }
            else
            {
                return abs;
            }
        }
    }
}
