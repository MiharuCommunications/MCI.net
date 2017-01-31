namespace Miharu.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public static class KeyExtensions
    {

        public static bool IsNumericKey(this Key key)
        {
            return (Key.D0 <= key && key <= Key.D9) || (Key.NumPad0 <= key && key <= Key.NumPad9);
        }
    }
}
