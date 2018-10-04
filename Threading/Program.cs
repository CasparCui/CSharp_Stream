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
            var demo = new ThreadingDemo();
            demo.StratThreadingDemo();
            Thread.Sleep(1000);

            Console.WriteLine("MainDemo");
        }
    }
}