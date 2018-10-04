using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;

namespace Caspar.CSharpTest
{
    public class ThreadingDemo
    {
        private object Locker => new object();
        private int integerData;

        public void Threading1()
        {
            for (int i = 0; i < 10; i++)
            {
                WriteLine("ThreadingDemo1");
                Thread.Sleep(1);
            }
        }

        public void ThreadingNoLockDemo(string threadNumber)
        {
            for (int i = 0; i < 5; i++)
            {
                integerData++;
                Thread.Sleep(1);
                WriteLine($"Thread {threadNumber} make integer data to {integerData}");
            }

        }
        /// <summary>
        /// 这个方法实际上是无法锁住的，因为尽管加锁了，但是锁住的不是类的实例，故无法保证integer的值连续自增。
        /// </summary>
        /// <param name="threadNumber"></param>
        public void TreadingLockDemo(string threadNumber)
        {
            for (int i = 0; i < 5; i++)
            {
                lock (Locker)
                {

                    integerData++;
                    Thread.Sleep(2);
                    WriteLine($"Thread {threadNumber} make integer data to {integerData}");
                }
            }
        }
        /// <summary>
        /// 这个方法就可以锁住了。
        /// </summary>
        /// <param name="threadNumber"></param>
        public void TreadingLockDemo2(string threadNumber)
        {

            for (int i = 0; i < 5; i++)
            {
                lock (this)
                {
                    integerData++;
                    Thread.Sleep(200);
                    WriteLine($"Thread {threadNumber} make integer data to {integerData}");
                }
            }
        }
        public void StratThreadingDemo()
        {

            Thread t1 = new Thread(() => TreadingLockDemo2("1"));
            Thread t2 = new Thread(() => TreadingLockDemo2("2"));
            Thread t3 = new Thread(() => TreadingLockDemo2("3"));
            Thread t4 = new Thread(() => TreadingLockDemo2("4"));
            Thread t5 = new Thread(() => TreadingLockDemo2("5"));
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            Thread.Sleep(300);
            t3.Abort();
            Thread.Sleep(300);
            t2.Abort();
            Thread.Sleep(100);
            
            //Thread.ResetAbort();
        }
    }
}
