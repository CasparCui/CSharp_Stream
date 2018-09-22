using System;
using System.Collections;
using System.Collections.Generic;

namespace Caspar.CSharpTest
{
    internal class GenericDemo<T> : IEnumerable<T>
    {
        public T[] array;

        public GenericDemo(int size)
        {
            Array = new T[size];
        }

        public T[] Array { get => array; set => array = value; }

        //public IEnumerator GetEnumerator()
        //{
        //    return ((IEnumerable<T>) Array).GetEnumerator();
        //}

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
            {
                yield return array[i];
            }
        }

        public T GetItem(int index)
        {
            return Array[index];
        }

        public void SetItem(int index, T value)
        {
            Array[index] = value;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>) Array).GetEnumerator();
        }
    }

    internal class GenericTester
    {
        public static void GenericDemoTestInt()
        {
            GenericDemo<int> generic = new GenericDemo<int>(5);
            for (int i = 0; i < 5; i++)
            {
                generic.SetItem(i, (i + 1));
            }
            foreach (int i in generic)
            {
                System.Console.WriteLine(i);
            }
        }

        public static void GenericDemoTestString()
        {
            GenericDemo<string> generic = new GenericDemo<string>(5);
            for (int i = 0; i < 5; i++)
            {
                generic.SetItem(i, ((char) (i + 97)).ToString());
            }
            foreach (string s in generic)
            {
                System.Console.WriteLine(s);
            }
        }

        public static void GenericeDemoTestWhereTheObjectIsComparable()
        {
            GenericConditionsDemo<ComparDemo> demo = new GenericConditionsDemo<ComparDemo>();
            ComparDemo demo1 = new ComparDemo() { DetaDemo = 3 };
            ComparDemo demo2 = new ComparDemo() { DetaDemo = 5 };
            ComparDemo demo3 = new ComparDemo() { DetaDemo = 0 };
            ComparDemo demo4 = null;
            Console.WriteLine(demo.Compare(demo1, demo2));
            Console.WriteLine(demo.Compare(demo1, demo3));
            Console.WriteLine(demo.Compare(demo1, demo4));
            Console.WriteLine(demo.Compare(demo1, demo1));
            Console.WriteLine(demo.CompareToDefault(demo1, demo3));
        }
    }

    internal class GenericConditionsDemo<T> where T : IComparable<T>
    {
        public int Compare(T first, T second) => first.CompareTo(second);

#pragma warning disable CS0693 // 类型参数与外部类型中的类型参数同名

        public int CompareToDefault<T>(T value, T value2) where T : IComparable<T> => value.CompareTo(value2);

#pragma warning restore CS0693 // 类型参数与外部类型中的类型参数同名
    }

    internal class ComparDemo : IComparable<ComparDemo>
    {
        public int DetaDemo = 1;

        public int CompareTo(ComparDemo obj)
        {
            ComparDemo tempObj = obj as ComparDemo;
            if (tempObj != null)
            {
                ComparDemo comparDemo = this;
                return tempObj.DetaDemo == comparDemo.DetaDemo ? 0 : tempObj.DetaDemo > comparDemo.DetaDemo ? 1 : -1;
            }
            else
            {
                return 1;
            }
        }
    }
}