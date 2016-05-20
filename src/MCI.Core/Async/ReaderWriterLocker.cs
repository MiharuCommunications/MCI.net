//-----------------------------------------------------------------------
// <copyright file="ReaderWriterLocker.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Miharu.Async;

    public class ReaderWriterLocker : IDisposable
    {
        private bool disposed;
        private readonly ReaderWriterLockSlim locker;

#if DEBUG
        private ThreadSafeCounter readLockCount;
        private ThreadSafeCounter writeLockCount;
        private const int maxCounter = 20;
#endif

        public ReaderWriterLocker()
        {
            this.disposed = false;
            this.locker = new ReaderWriterLockSlim();
#if DEBUG
            this.readLockCount = new ThreadSafeCounter();
            this.writeLockCount = new ThreadSafeCounter();
#endif
        }

        public Try Write(Func<Try> f)
        {
            var result = Try.Fail(new NotImplementedException());
            this.locker.EnterWriteLock();
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
                this.locker.ExitWriteLock();
            }

            return result;
        }


        public Task<Try> WriteAsync(Func<Try> f)
        {
            var result = Try.Fail(new NotImplementedException());
            var task = new Task<Try>(() => result);

            Task.Factory.StartNew(() =>
            {
                if (!this.locker.IsWriteLockHeld)
                {
#if DEBUG
                    this.writeLockCount.Increment();
#endif
                    if (this.disposed)
                    {
                        return;
                    }

                    this.locker.EnterWriteLock();
#if DEBUG
                    this.writeLockCount.Decrement();
                    if (maxCounter < this.writeLockCount.Counter)
                    {
//                        Debuggers.AddError("ReaderWriterLocker.WriteAsync の Counter が増えています: Counter =" + this.writeLockCount.Counter.ToString());
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
                        this.locker.ExitWriteLock();
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
            var result = Try<T>.Fail(new NotImplementedException());

            this.locker.EnterReadLock();
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
                this.locker.ExitReadLock();
            }
            return result;
        }


        public Task<Try<T>> ReadAsync<T>(Func<Try<T>> f)
        {
            var result = Try<T>.Fail(new NotImplementedException());
            var task = new Task<Try<T>>(() => result);

            Task.Factory.StartNew(() =>
            {
                if (!this.locker.IsWriteLockHeld)
                {
#if DEBUG
                    this.readLockCount.Increment();
#endif
                    if (this.disposed)
                    {
                        return;
                    }

                    this.locker.EnterWriteLock();
#if DEBUG
                    this.readLockCount.Decrement();
                    if (maxCounter < this.readLockCount.Counter)
                    {
//                        Debuggers.AddError("ReaderWriterLocker.ReadAsync の Counter が増えています: Counter =" + this.readLockCount.Counter.ToString());
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
                        this.locker.ExitWriteLock();
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
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.locker.Dispose();
            }

            this.disposed = true;
        }
    }
}
