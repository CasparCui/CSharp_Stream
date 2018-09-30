using System;

namespace Caspar.CSharpTest
{
    public class DataDemo2 : IEquatable<DataDemo2>
    {
        public int IntData { get; set; } = 0;
        public string StringData { get; set; } = "Test";
        public string StringData2 { get; private set; } = "Test2";

        public bool Equals(DataDemo2 other)
        {
            if (IntData == other.IntData && StringData == other.StringData && StringData2 == other.StringData2)
            {
                return true;
            }
            return false;
        }
    }
}