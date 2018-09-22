using System;

namespace Caspar.CSharpTest
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    internal class AttributeDemo : Attribute
    {
        public void AttribulteUsageDemo()
        {
        }
    }
}