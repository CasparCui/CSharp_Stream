using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedFeature.cs
{
    

    class DynamicDemo
    {
        static public void DoDynamic()
        {
            dynamic dynamicDemoBase = new DynamicDemoBase();
            var s = dynamicDemoBase.GetDemoData();
            //s= dynamicDemoBase.AsDynamic().GetDemoDataForDynamic();
            var method = typeof(DynamicDemoBase).GetMethod("GetDemoDataForDynamic",System.Reflection.BindingFlags.NonPublic|BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var obj = (method.Invoke(dynamicDemoBase, null));
        }
        class DynamicDemoBase
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
