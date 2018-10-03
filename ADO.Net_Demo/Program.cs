using System;

namespace Caspar.CSharpTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            DataSetDemo.SqlDataSetToXmlDemo(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=TestaADO.Net;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}