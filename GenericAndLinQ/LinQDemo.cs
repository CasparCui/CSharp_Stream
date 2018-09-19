using System;
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
        static BasicCollect<DataDemo> GetADemoCollectionForTest()
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
            var dataFromLinQ2 = Enumerable  .Where(dataDemoCollection, demo => demo.NullInt != null && demo.Property1 == "444")
                                            .Where(demo => demo.Property2 != null);
            foreach (DataDemo data in dataFromLinQ2)
            {
                Console.WriteLine("demount: {0}, demopro1: {1}", data.NullInt ?? 9999, data.Property1 ?? "null");
            }
        }
    }
}