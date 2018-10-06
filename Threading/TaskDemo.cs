using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caspar.CSharpTest
{
    class TaskDemo
    {
        public static void TaskFactoryDemo()
        {
            var taskFactory = new TaskFactory();
            var t1 = taskFactory.StartNew(TaskMethod);
            var t2 = taskFactory.StartNew(TaskMethod);

        }

        public static void TaskMethod()
        {
            Console.WriteLine("Do A Task Sync Thing");
        }
        public static int TaskReturnIntegerMethod(int inti)
        {
            Console.WriteLine($"Do A Task Return Integer Thing {inti}");
            return 8;
        }
        public static int TaskReturnIntegerMethod2(int inti)
        {
            Console.WriteLine($"Do A Task Return Integer Thing2 {inti}");
            return 8;
        }
        public static void TaskAddDemo(int inti)
        {
            var t = new Task<int>(() =>
            {
                int i = 10;
                i++;
                Console.WriteLine(i);
                Console.WriteLine(inti);
                
                return i;
            });
            var t2 = t.ContinueWith((task) => TaskReturnIntegerMethod(inti));
            //var t3 = Task.Factory.StartNew(() => TaskReturnIntegerMethod2(inti));
            // 这样的用法是不可以的，因为 t3 的start 会直接在方法中完成了，这样会直接导致 t2 的先行启动。
            t2.ContinueWith((task) => TaskReturnIntegerMethod2(inti));
            t.Start();
        }
        public static Task DoTask(int i)
        {
            return Task.Run(() => {
                Console.WriteLine("task step1 of " + i.ToString());
            }).ContinueWith((preTask) => {
                Console.WriteLine("task step2 of " + i.ToString());
            }).ContinueWith((preTask) => {
                Console.WriteLine("task step3 of " + i.ToString());
            });

        }
    }
}
