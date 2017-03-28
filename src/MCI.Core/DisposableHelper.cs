namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DisposableHelper
    {
        public static IDisposable Create(Action action)
        {
            return new Disposable(action);
        }

        public static IDisposable Create(Func<Action> f)
        {
            return new Disposable(f());
        }
    }
}
