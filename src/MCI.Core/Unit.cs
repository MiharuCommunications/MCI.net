namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class Unit
    {
        private Unit()
        {
        }

        public static readonly Unit Instance;

        static Unit()
        {
            Instance = new Unit();
        }
    }
}
