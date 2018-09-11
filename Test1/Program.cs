using System;
using System.Collections.Generic;

namespace Caspar.CSharpTest
{
    internal class Program
    {
        private static List<String> s5 = new List<string>();

        public static List<string> S5 { get => s5; set => s5 = value; }

        public static Predicate<object> GetS1()
        {
            return s1;
        }

        public void SetS1(Predicate<object> value)
        {
            s1 = value;
        }

        private static Predicate<object> s1;

        private static void Main(string[] args)
        {
            using (var fileStreamTest = new TestFileStreamC(@"C:\Users\caspar\Desktop\TKYSPWFD11-20180523-1531.log"))
            {
                for (int i = 0; i < 10; i++)
                {
                    System.Console.WriteLine(fileStreamTest.ReadLine());
                }
            }
        }
    }
}