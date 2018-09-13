using System;

namespace AdvancedFeature.cs
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FormatDemo demo = new FormatDemo();

            var formatDemoFormater = new FormatDemoFormater();
            Console.WriteLine(formatDemoFormater.Format("+", demo, null));
            Console.WriteLine(formatDemoFormater.Format("Add", demo, null));
            Console.WriteLine(formatDemoFormater.Format("-", demo, null));
            Console.WriteLine(formatDemoFormater.Format("menas", demo, null));
            Console.WriteLine(demo.ToString());
            Console.ReadLine();


        }
    }
}