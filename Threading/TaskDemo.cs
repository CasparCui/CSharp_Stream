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
    }
}
