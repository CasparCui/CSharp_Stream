using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
}
