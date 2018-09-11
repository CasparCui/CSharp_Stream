using System;
using System.Xml;

namespace Caspar.CSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (XmlReader reader = System.Xml.XmlReader.Create(@"D:\Github\CSharp_Stream1\XmlTest1\xml1.xml"))
            {
                reader.Read();
                var s1 = reader.ReadInnerXml();
                reader.ReadToDescendant(@"ID");
                s1 = reader.ReadOuterXml();
            }
        }
    }
}
