using System;

namespace Caspar.CSharpTest
{
    internal class Program
    {
#pragma warning disable RECS0154 // Parameter is never used

        private static void Main(string[] args)
#pragma warning restore RECS0154 // Parameter is never used
        {
            XmlDocumentDemo.XmlDocumentWriteDemo("./xml1.xml");
            Console.ReadLine();
        }
    }
}