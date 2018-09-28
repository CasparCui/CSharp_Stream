﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Caspar.CSharpTest
{
    /// <summary>
    /// 支持 .NET Framework 类层次结构中的所有类，并为派生类提供低级别服务。
    /// 这是 .NET Framework 中所有类的最终基类；它是类型层次结构的根。
    ///
    /// 若要浏览此类型的.NET Framework 源代码，请参阅Reference Source。
    /// </summary>
    public static class LinQDemo
    {
        private static BasicCollect<DataDemo> GetADemoCollectionForTest()
        {
            var demoData1 = new DataDemo() { NullInt = null, Property1 = "333" };
            var demoData2 = new DataDemo() { NullInt = 1, Property1 = "444" };
            var demoData3 = new DataDemo() { NullInt = 12, Property1 = "555" };
            var demoData4 = new DataDemo() { NullInt = 123, Property1 = null };
            BasicCollect<DataDemo> basicCollect = new BasicCollect<DataDemo>
            {
                demoData1,
                demoData2,
                demoData3,
                demoData4
            };
            return basicCollect;
        }

        private static BasicCollect<DataDemo2> GetADemo2CollectionForTest()
        {
            var demoData1 = new DataDemo2() { IntData = 0, StringData = "333" };
            var demoData2 = new DataDemo2() { IntData = 1, StringData = "444" };
            var demoData3 = new DataDemo2() { IntData = 12, StringData = "555" };
            var demoData4 = new DataDemo2() { IntData = 133, StringData = "222" };
            BasicCollect<DataDemo2> basicCollect = new BasicCollect<DataDemo2>
            {
                demoData1,
                demoData2,
                demoData3,
                demoData4
            };
            return basicCollect;
        }

        public static void DoALinQDemo()
        {
            BasicCollect<DataDemo> dataDemoCollection = GetADemoCollectionForTest();
            var dataFromLinQ = from demo in dataDemoCollection
                               where demo.NullInt != null
                               select demo;
            foreach (DataDemo data in dataFromLinQ)
            {
                Console.WriteLine("demount: {0}, demopro1: {1}", data.NullInt ?? 9999, data.Property1 ?? "null");
            }
            var dataFromLinQ2 = Enumerable.Where(dataDemoCollection, demo => demo.NullInt != null && demo.Property1 == "444")
                                            .Where(demo => demo.Property2 != null);
            foreach (DataDemo data in dataFromLinQ2)
            {
                Console.WriteLine("demount: {0}, demopro1: {1}", data.NullInt ?? 9999, data.Property1 ?? "null");
            }
        }

        public static void DoALinQJoinDemo()
        {
            BasicCollect<DataDemo> dataDemoCollection1 = GetADemoCollectionForTest();
            BasicCollect<DataDemo2> dataDemoCollection2 = GetADemo2CollectionForTest();
            var dataFromLinq = from demo in dataDemoCollection1
                               join demo2 in dataDemoCollection2
                               on demo.NullInt equals demo2.IntData

                               where demo.Property1 == demo2.StringData
                               select new
                               {
                                   IntDataTemp = demo2.IntData,
                                   StringDataTemp = demo.Property2
                               };
            var dataFromLambda = dataDemoCollection1
                .Join(dataDemoCollection2, i => i.NullInt, j => j.IntData, (i, j) => new { demo = i, demo2 = j })
                .Where(d => d.demo.Property1 == d.demo2.StringData)
                .Select(j => new { IntDataTemp = j.demo2.IntData, StringDataTemp = j.demo.Property2 });
            Dictionary<int, string> dic = dataFromLinq.ToDictionary(k => k.IntDataTemp, v => v.StringDataTemp);
            Dictionary<int, string> dic2 = dataFromLambda.ToDictionary(k => k.IntDataTemp, v => v.StringDataTemp);
        }

        public static void DoALinQOrderByDemo()
        {
            BasicCollect<DataDemo2> dataDemoCollection = GetADemo2CollectionForTest();
            var dataForLinq = from demo in dataDemoCollection
                              orderby demo.IntData
                              select new
                              {
                                  demo.IntData,
                                  StringData1 = demo.StringData,
                                  demo.StringData2
                              };
            var dataForLambda = dataDemoCollection.OrderBy(k => k.IntData).Select(j => new { j.IntData, StringData1 = j.StringData, j.StringData2 });
            foreach (var demo in dataForLinq)
            {
                Console.WriteLine("IntData: {0}, StringData1: {1}, StringData2: {2}", demo.IntData, demo.StringData1, demo.StringData2);
            }
            foreach (var demo in dataForLambda)
            {
                Console.WriteLine("`IntData: {0}, StringData1: {1}, StringData2: {2}", demo.IntData, demo.StringData1, demo.StringData2);
            }
        }

        public static void DoALinqOfTypeAndCastDemo()
        {
            var objArray = new object[] { 123, "234", 345, "456" };
            try
            {
                var stringArray = objArray.Cast<String>().ToArray();
                foreach (var s in stringArray)
                {
                    Console.WriteLine("Cast");
                    Console.WriteLine(s);
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e);
            }
            var stringArray2 = objArray.OfType<String>().ToArray();
            foreach (var s in stringArray2)
            {
                Console.WriteLine("OfType");
                Console.WriteLine(s);
            }
        }

        public static void DoALinqSkipAndWhileSkipDemo()
        {
            //BasicCollect<DataDemo2> dataDemoCollection = GetADemo2CollectionForTest();
            //var skipListByLambda = dataDemoCollection.Skip(3);
            var dataDemoCollection = new int[] { -1,1,2,3};
            int[] datademo = { -1, 2, 3, 5 };
            IEnumerable<int> skipWhileByLambda = datademo.OrderByDescending(s=>s).SkipWhile(grade => grade >= 0);
            foreach (var i in skipWhileByLambda)
            {
                Console.Write(i + ",");
            }
            int[] grades = { 59, 82, 70, 56, 92, 98, 85 };

            IEnumerable<int> lowerGrades =
                datademo
                .OrderByDescending(grade => grade)
                .SkipWhile(grade => grade >= 0);

            Console.WriteLine("All grades below 80:");
            foreach (int grade in lowerGrades)
            {
                Console.WriteLine(grade);
            }
        }
    }
}