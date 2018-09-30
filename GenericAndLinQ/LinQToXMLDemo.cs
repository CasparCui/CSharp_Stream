using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System;

namespace Caspar.CSharpTest
{
    public static class LinQToXMLDemo
    {
        public static XElement CreateLinqXmlTree()
        {
            XElement xEle = new XElement("TestRoot", new XElement[]{

                new XElement("Element",new object [] {
                    new XAttribute("TestAttribute1","hehe1"),
                    new XElement("TestSubSub1", new XAttribute[] {
                        new XAttribute("TestAttributeSub2","hehe2"),
                        new XAttribute("TestAttributeSub3","hehe3"),
                    })
                }),
                new XElement("Element", new object[]{
                    new XElement("Element2Sub1",new XAttribute[] {
                        new XAttribute("TestAttributeSub2","hehe2"),
                        new XAttribute("TestAttributeSub3","hehe3"),
                    }),
                    new XAttribute("elementAttribulte","xixi")
                }),
                new XElement("Element", new object[]{
                    new XElement("Element3Sub1"),
                    new XElement("Element3Sub2", DateTime.Now.ToShortTimeString()),
                    new XAttribute("elementAttribulte","xixi")
                }),
                new XElement("Element", new object[]{
                    new XElement("Element2Sub3",new object[] {
                        new XAttribute("TestAttributeSub2","hehe2"),
                        new XAttribute("TestAttributeSub3","hehe3"),
                        new XElement("ElementSubSub",new XElement("Element3", new object[]{
                            new XElement("Element2SubSubSub","aiyowoqu"),
                            new XAttribute("elementAttribulte","xixi3"),
                        }))

                    }),
                    new XAttribute("elementAttribulte","xixi3")
                }),
                new XElement("Element", new object[]{
                    new XElement("Element2Sub4"),
                    new XAttribute("elementAttribulte","xixi4")
                })
            });
            Console.WriteLine(xEle.ToString());
            return xEle;

        }

        public static void SelectANodeByLinQ(XElement xEle)
        {
            var xEleDeepCopy = new XElement(xEle);
            var xEleEnumerable = xEleDeepCopy.Elements("Element");
            var selectEleEnumerable = from ele in xEleDeepCopy.Elements("Element")
                                      let attri = ele.Attribute("elementAttribulte")
                                      where attri != null && attri.Value == "xixi"
                                      select new { attri.Value ,ele};
            foreach (var ele in selectEleEnumerable)
            {
                Console.WriteLine(ele.Value + " " + ele.ele.ToString() );
            }

            var selectASubElement = from ele in xEleEnumerable
                                    let subEle = ele.Element("Element2Sub3")
                                    where subEle != null
                                    let subAttri = subEle.Attribute("TestAttributeSub2")
                                    where subAttri != null
                                    select new { ele, InerXML = subEle.ToString(), subAttri.Name, subAttri.Value };
                                    
            foreach (var ele in selectASubElement)
            {
                Console.WriteLine(ele.ele);
                Console.WriteLine(ele.InerXML);
                Console.WriteLine(ele.Name + ele.Value);
            }


        }
        public static void AddANodeIntoXmlByLinQ(XElement xEle)
        {
            var xEleDeepCopy = new XElement(xEle);
            System.Collections.Generic.IEnumerable<XElement> selectedXElement;
            selectedXElement = from ele in xEleDeepCopy.Elements()
                               let attri = ele.Attribute("elementAttribulte")
                               where attri != null && attri.Value == "xixi3"
                               select ele;
            var selectedXElementLambda = xEleDeepCopy.Elements()
                                        .Where(x => x.Attribute("elementAttribulte") != null && x.Attribute("elementAttribulte").Value == "xixi3");
            var tempXElement = new XElement("ADD", new object[] { new XElement("abc2", new object[] { new XAttribute("attri1", "xixixi"), new XAttribute("attri2", "hehehe") }) });
            var tempXElement1 = new XElement("ADDAFTERSELF", new object[] { new XElement("abc2", new object[] { new XAttribute("attri1", "xixixi"), new XAttribute("attri2", "hehehe") }) });
            var tempXElement2 = new XElement("AddAnnotation", new object[] { new XElement("abc2", new object[] { new XAttribute("attri1", "xixixi"), new XAttribute("attri2", "hehehe") }) });
            var tempXElement3 = new XElement("AddBeforeSelf", new object[] { new XElement("abc2", new object[] { new XAttribute("attri1", "xixixi"), new XAttribute("attri2", "hehehe") }) });
            var tempXElement4 = new XElement("AddFirst", new object[] { new XElement("abc2", new object[] { new XAttribute("attri1", "xixixi"), new XAttribute("attri2", "hehehe") }) });
            if (selectedXElement != null)
            {
                foreach (XElement node in selectedXElement)
                {
                    node.Add(tempXElement);
                    node.AddAfterSelf(tempXElement1);
                    node.AddAnnotation(tempXElement2);
                    node.AddBeforeSelf(tempXElement3);
                    node.AddFirst(tempXElement4);
                    
                    Console.WriteLine(xEleDeepCopy);
                }
            }

        }
        public static void RemoveNodeFromXmlByLinQ(XElement xEle)
        {
            var xEleDeepCopy = new XElement(xEle);
            var selectedXElementByLambda = xEleDeepCopy.Elements("Element")
                                           .Where(x => x.Element("Element2Sub3") != null);
            foreach( XElement ele in selectedXElementByLambda)
            {
                //ele.RemoveAll();
                //ele.RemoveAttributes();
                //ele.RemoveNodes();
                ele.Remove();
                Console.WriteLine(xEleDeepCopy);
            }
        }
        public static void SetAttributeByLinQ(XElement xEle)
        {
            var xEleDeepCopy = new XElement(xEle);
            var selectedXElementLambda = xEleDeepCopy.Elements()
                                        .Where(x => x.Attribute("elementAttribulte") != null 
                                                 && x.Attribute("elementAttribulte").Value == "xixi3");
            foreach (XElement ele in selectedXElementLambda)
            {
                ele.SetAttributeValue("elementAttribulte", "hehehe3");
                ele.SetAttributeValue("elementAttribulte2", "hehehe4");

            }
            Console.WriteLine(xEleDeepCopy);
        }
        public static void ReplaceElementByLinQ(XElement xEle)
        {
            var xEleDeepCopy = new XElement(xEle);
            var selectedXElementLambda = xEleDeepCopy.Elements()
                                        .Where(x => x.Attribute("elementAttribulte") != null
                                                 && x.Attribute("elementAttribulte").Value == "xixi3");
            var element = new XElement("heheda", new object[] { new XElement("xixixi", "hehehe") });
            var attribute = new XAttribute("xixixix", "animi");
            foreach(XElement ele in selectedXElementLambda)
            {
                //ele.ReplaceAll(element);
                //ele.ReplaceNodes(element);
                ele.ReplaceAttributes(attribute);
            }
            Console.WriteLine(xEleDeepCopy);
        }
        
    }
}
