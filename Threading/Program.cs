using System;
using System.Threading;

namespace Caspar.CSharpTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            if (args == null)
                throw new ArgumentNullException(nameof(args));
            for (int i = 0; i < 10; i++)
            {
                TaskDemo.TaskAddDemo(i);
            }
            Thread.Sleep(2000);
            Console.WriteLine("MainDemo");
        }
    }
}