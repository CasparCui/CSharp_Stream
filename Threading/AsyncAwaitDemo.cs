using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Caspar.CSharpTest
{
    public static class AsyncAwaitDemo
    {
        public static async Task AsyncDemo()
        {
            Console.WriteLine("Main Thread");
            Console.WriteLine($"Sub Thread { DoAsyncThingDemo().Result}");
            Console.WriteLine("Main Thread2");
        }

        public static Task<string> DoAsyncThingDemo()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Sync!!");
                Thread.Sleep(1000);
                return "Async";
            });
        }
    }

    public static class ParallelDemo
    {
        public static void DoParallelForDemo()
        {
            var intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Parallel.For(0, intList.Count, i =>
            {
                Console.WriteLine($"DoParallel For int = {intList[i]}");
            });
        }

        public static void DoParallelForeachDemo()
        {
            var intList = new List<int>() { 1, 22, 3, 4, 5, 6, 7, 8, 9, 4_10 };
            Parallel.ForEach(intList, i =>
            {
                Console.WriteLine($"DoParallel Foreach int = {i}");
            });
        }

        public static void DoParallelInvokeDemo()
        {
            Parallel.Invoke(DoParallelForDemo, DoParallelForeachDemo);
        }

        public static void DoBreakParallelForDemo()
        {
            var intList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int k = 0; k < 100; k++)
            {
                var result = Parallel.For(0, intList.Count, (i, pls) =>
                {
                    Task.Delay(10).Wait();
                    if (i > 5)
                    {
                        Console.WriteLine($"For is break, i = {i}, pls status = {pls.IsStopped}");
                        pls.Break();
                    }
                    // 从执行结果中可以看出，在请求 break 之后，并非直接结束循环，而是会完成循环中的所有内容后，下一次动作才不会执行。
                    //Console.WriteLine($"For is runing, i = {i}");
                });
                Console.WriteLine($"{result.IsCompleted},  {result.LowestBreakIteration}");
            }
        }

        public static void DoParallelContinuumFuncAction()
        {
            var intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int b = 0; b < 100; b++)
            {
                int v = 0;
                var result = Parallel.For(0, intList.Count, () => { return 1; }, (i, j, k) =>
                    {
                        Task.Delay(1).Wait();
                        k += intList[i];
                        return k;
                    }, (x)=>
                    {
                        Interlocked.Add(ref v, x);
                    });
                Console.WriteLine(v);
            }
        }
    }
}