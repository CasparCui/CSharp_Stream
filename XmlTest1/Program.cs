﻿using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Caspar.CSharpTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            XmlReaderAndWriter.UseXmlWriter();
            Console.ReadLine();
        }
    }
}