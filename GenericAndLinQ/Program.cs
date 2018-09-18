using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caspar.CSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var demoData1 = new DataDemo() { NullInt = null, Property1 = "333" };
            var demoData2 = new DataDemo() { NullInt = 1, Property1 = "444" };
            var demoData3 = new DataDemo() { NullInt = 12, Property1 = "555" };
            var demoData4 = new DataDemo() { NullInt = 123, Property1 = null };
            var demoList = new BasicCollect<DataDemo>();
            demoList.Add(demoData1);
            demoList.Add(demoData2);
            demoList.Add(demoData3);
            demoList.Add(demoData4);
            foreach(DataDemo demo in demoList)
            {
                Console.WriteLine("demoint: {0}, demopro1: {1}", demo.NullInt ?? 9999, demo.Property1 ?? "null");
            }


        }
    }
}
