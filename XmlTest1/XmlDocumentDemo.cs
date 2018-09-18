using System;
using System.Xml;

namespace Caspar.CSharpTest
{
    internal class XmlDocumentDemo
    {
        public static void XmlDocumentReadDemo(string path)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var rootXmlElement = xmlDoc.DocumentElement;
            var xmlElementTemp = rootXmlElement.FirstChild;
            var xmlNodeList = rootXmlElement.GetElementsByTagName("ID");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                var value = (xmlNode as XmlElement).GetAttributeNode("value").Value;
                Console.WriteLine(value);
            }
        }

        public static void XmlDocumentWriteDemo(string path)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var rootElement = xmlDoc.DocumentElement;
            XmlNodeList xmlNodeList = rootElement.GetElementsByTagName("ID");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                var tempElement = xmlDoc.CreateElement("IDT");
                tempElement.InnerText = "haha";
                tempElement.Attributes.Append(xmlDoc.CreateAttribute("xixi"));
                xmlNode.AppendChild(tempElement as XmlNode);
            }
            xmlDoc.Save(path);
        }
    }
}