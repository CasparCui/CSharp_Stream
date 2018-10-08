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
            ParallelDemo.DoParallelContinuumFuncAction();
            Console.ReadLine();
        }
    }
}