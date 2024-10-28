//-----------------------------------------------------------------------
// <copyright file="ReaderWriterLocker.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ReaderWriterLocker : IDisposable
    {
        private bool _disposed;
        private readonly ReaderWriterLockSlim _locker;

#if DEBUG
        private ThreadSafeCounter readLockCount;
        private ThreadSafeCounter writeLockCount;
        private const int maxCounter = 20;
#endif

        public ReaderWriterLocker()
        {
            _disposed = false;
            _locker = new ReaderWriterLockSlim();
#if DEBUG
            readLockCount = new ThreadSafeCounter();
            writeLockCount = new ThreadSafeCounter();
#endif
        }

        public Try Write(Func<Try> f)
        {
            var result = TryHelper.ReturnNotImplementedException();
            _locker.EnterWriteLock();
            try
            {
                result = f();
            }
            catch (Exception e)
            {
                result = Try.Fail(e);

            }
            finally
            {
                _locker.ExitWriteLock();
            }

            return result;
        }


        public Task<Try> WriteAsync(Func<Try> f)
        {
            var result = TryHelper.ReturnNotImplementedException();
            var task = new Task<Try>(() => result);

            Task.Factory.StartNew(() =>
            {
                if (!_locker.IsWriteLockHeld)
                {
#if DEBUG
                    writeLockCount.Increment();
#endif
                    if (_disposed)
                    {
                        return;
                    }

                    _locker.EnterWriteLock();
#if DEBUG
                    writeLockCount.Decrement();
                    if (maxCounter < writeLockCount.Counter)
                    {
                        //                        Debuggers.AddError("ReaderWriterLocker.WriteAsync の Counter が増えています: Counter =" + writeLockCount.Counter.ToString());
                    }
#endif

                    try
                    {
                        result = f();
                    }
                    catch (Exception e)
                    {
                        result = Try.Fail(e);
                    }
                    finally
                    {
                        _locker.ExitWriteLock();
                    }
                }
                else
                {
                    result = f();
                }

                task.RunSynchronously();
            });


            return task;
        }

        public Try<T> Read<T>(Func<Try<T>> f)
        {
            var result = TryHelper.ReturnNotImplementedException<T>();

            _locker.EnterReadLock();
            try
            {
                result = f();
            }
            catch (Exception e)
            {
                result = Try<T>.Fail(e);
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return result;
        }


        public Task<Try<T>> ReadAsync<T>(Func<Try<T>> f)
        {
            var result = TryHelper.ReturnNotImplementedException<T>();
            var task = new Task<Try<T>>(() => result);

            Task.Factory.StartNew(() =>
            {
                if (!_locker.IsWriteLockHeld)
                {
#if DEBUG
                    readLockCount.Increment();
#endif
                    if (_disposed)
                    {
                        return;
                    }

                    _locker.EnterWriteLock();
#if DEBUG
                    readLockCount.Decrement();
                    if (maxCounter < readLockCount.Counter)
                    {
                        //                        Debuggers.AddError("ReaderWriterLocker.ReadAsync の Counter が増えています: Counter =" + readLockCount.Counter.ToString());
                    }
#endif

                    try
                    {
                        result = f();
                    }
                    catch (Exception e)
                    {
                        result = Try<T>.Fail(e);
                    }
                    finally
                    {
                        _locker.ExitWriteLock();
                    }
                }
                else
                {
                    result = f();
                }

                task.RunSynchronously();
            });


            return task;
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _locker.Dispose();
            }

            _disposed = true;
        }
    }
}
