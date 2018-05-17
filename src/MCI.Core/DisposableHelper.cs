namespace Miharu
{
    using System;

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
