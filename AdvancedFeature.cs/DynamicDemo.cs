using System.Reflection;

namespace Caspar.CSharpTest
{
    internal class DynamicDemo
    {
        static public void DoDynamic()
        {
            dynamic dynamicDemoBase = new DynamicDemoBase();
            var s = dynamicDemoBase.GetDemoData();
            //s= dynamicDemoBase.AsDynamic().GetDemoDataForDynamic();
            var method = typeof(DynamicDemoBase).GetMethod("GetDemoDataForDynamic", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var obj = (method.Invoke(dynamicDemoBase, null));
        }

        private class DynamicDemoBase
        {
            public string GetDemoData()
            {
                return "2333";
            }

            private string GetDemoDataForDynamic()
            {
                return "2333";
            }
        }
    }
}