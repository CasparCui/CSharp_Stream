using System;
using System.Collections;
using System.Collections.Generic;

namespace Caspar.CSharpTest
{
    internal class BasicCollect<T> : ICollection<T>
                            where T : IEquatable<T>
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

        public int Count => demo.Count;

        public bool IsReadOnly => demo.IsReadOnly;

        public void Add(T item) => demo.Add(item);

        public void Clear() => demo.Clear();

        public bool Contains(T item) => demo.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => demo.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => demo.GetEnumerator();

        public bool Remove(T item) => demo.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => demo.GetEnumerator();
    }
}