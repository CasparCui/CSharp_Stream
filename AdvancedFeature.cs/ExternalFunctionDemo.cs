using System;

namespace Caspar.CSharpTest
{
    public static class ExternalFunctionDemo
    {
        public static void Print(this string s)
        {
            Console.WriteLine(s);
        }
    }
}
