using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Caspar.CSharpTest
{
    internal class XmlReaderAndWriter
    {
        public void UseXmlReader()
        {
            using (XmlReader reader = System.Xml.XmlReader.Create("./xml1.xml"))
            {
                reader.Read();
                var s1 = reader.ReadInnerXml();
                reader.ReadToDescendant(@"ID");
                s1 = reader.ReadOuterXml();
            }
        }

        static public void UseXmlWriter()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var writersetting = new XmlWriterSettings();
                writersetting.Async = false;
                writersetting.Encoding = new UTF8Encoding(false);
                writersetting.Indent = true;
                writersetting.NewLineChars = Environment.NewLine;
                using (XmlWriter writer = XmlWriter.Create(ms, writersetting))
                {
                    writer.WriteStartDocument(false);
                    writer.WriteStartElement("Root");
                    writer.WriteStartElement("ID");
                    writer.WriteAttributeString("value", "23333");
                    // writer.WriteEndAttribute();
                    writer.WriteString("9039393");
                    writer.WriteEndElement();
                    writer.WriteStartElement("dog");
                    //写CData
                    writer.WriteCData("<strong>dog is dog</strong>");
                    writer.WriteEndElement();
                    writer.WriteComment("Comment");
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                string xml = Encoding.UTF8.GetString(ms.ToArray());
                var file = File.Create("./xml2.xml");
                file.Write(ms.ToArray(), 0, ms.ToArray().Length);
                Console.WriteLine(xml);
            }
        }
    }
}