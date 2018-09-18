using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caspar.CSharpTest
{ 
    class BasicCollect<T> : ICollection<T>,IEnumerable<T>
    {
        protected List<T> demo;

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
            for(int i = 0; i<demo.Count;i++)
            {
                yield return demo[i];
            }
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>) demo).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < demo.Count; i++)
            {
                yield return demo[i];
            }
        }
    }
}
