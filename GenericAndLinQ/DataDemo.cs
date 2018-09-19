using System;
using System.Collections.Generic;
using System.Text;

namespace Caspar.CSharpTest

{
    internal class DataDemo : IEquatable<DataDemo>
    {
        public string Property1 { get; set; } = "123";
        public string Property2 { get; } = "1234";
        public int? NullInt { get; set; }

        public bool Equals(DataDemo other)
        {
            if (this.Property1.Equals(other.Property1) && this.NullInt == other.NullInt)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is DataDemo)
            {
                return Equals(obj as DataDemo);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -1445159770;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Property1);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Property2);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(NullInt);
            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(DataDemo demo1, DataDemo demo2)
        {
            return EqualityComparer<DataDemo>.Default.Equals(demo1, demo2);
        }

        public static bool operator !=(DataDemo demo1, DataDemo demo2)
        {
            return !(demo1 == demo2);
        }

        public static DataDemo operator +(DataDemo demo1, DataDemo demo2)
        {
            DataDemo demoTemp = new DataDemo();
            var sb = new StringBuilder();
            sb.Append(demo1.Property1);
            sb.Append(demo2.Property1);
            demoTemp.Property1 = sb.ToString();
            if (demo1.NullInt != null && demo2.NullInt != null)
            {
                demoTemp.NullInt = demo1.NullInt + demo2.NullInt;
            }
            else
            {
                demoTemp.NullInt = demo1.NullInt ?? demo2.NullInt;
            }
            return demoTemp;
        }
    }
}