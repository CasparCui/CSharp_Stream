using System;

namespace AdvancedFeature.cs
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    internal class AttributeDemo : Attribute
    {
        public void AttribulteUsageDemo()
        {
        }
    }
}