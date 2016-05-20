namespace Miharu.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class SynchronizedMonitorableCollection<T> : IList<T>, ICollection, INotifyCollectionChanged
    {
        private ReaderWriterLockSlim locker;
        private List<T> list;

        public SynchronizedMonitorableCollection()
        {
            this.locker = new ReaderWriterLockSlim();
            this.list = new List<T>();
        }

        public SynchronizedMonitorableCollection(int size)
        {
            this.locker = new ReaderWriterLockSlim();
            this.list = new List<T>(size);
        }

        public SynchronizedMonitorableCollection(IEnumerable<T> source)
        {
            this.locker = new ReaderWriterLockSlim();
            this.list = new List<T>(list);
        }


        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
