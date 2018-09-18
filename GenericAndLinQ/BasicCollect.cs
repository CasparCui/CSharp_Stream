using System.Collections;
using System.Collections.Generic;

namespace Caspar.CSharpTest
{
    internal class BasicCollect<T> : ICollection<T>
    {
        protected ICollection<T> demo;

        public BasicCollect()
        {
            demo = new List<T>();
        }

        public BasicCollect(List<T> demo)
        {
            this.demo = demo;
        }

        public int Count => ((ICollection<T>) demo).Count;

        public bool IsReadOnly => ((ICollection<T>) demo).IsReadOnly;

        public void Add(T item)
        {
            ((ICollection<T>) demo).Add(item);
        }

        public void Clear()
        {
            ((ICollection<T>) demo).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>) demo).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>) demo).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return demo.GetEnumerator();
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>) demo).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return demo.GetEnumerator();
        }
    }
}