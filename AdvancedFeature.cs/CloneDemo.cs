using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Caspar.CSharpTest
{
    class CloneDemoDeta : IEquatable<CloneDemoDeta>
    {
        public string N { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as CloneDemoDeta);
        }

        public bool Equals(CloneDemoDeta other)
        {
            return other != null &&
                   N == other.N;
        }

        public override string ToString()
        {
            return N;
        }

        public override int GetHashCode()
        {
            return -789860487 + EqualityComparer<string>.Default.GetHashCode(N);
        }

        public static bool operator ==(CloneDemoDeta deta1, CloneDemoDeta deta2)
        {
            return EqualityComparer<CloneDemoDeta>.Default.Equals(deta1, deta2);
        }

        public static bool operator !=(CloneDemoDeta deta1, CloneDemoDeta deta2)
        {
            return !(deta1 == deta2);
        }
    }

    class CloneDemo : ICloneable
    {
        public int P { get; set; } = 1;
        public int Q { get; set; } = 2;
        public CloneDemoDeta DemoData { get; set; } = new CloneDemoDeta() { N = "123" };

        public object Clone() => MemberwiseClone();

        public CloneDemo DeepClone()
        {
            using (var objectStream = new System.IO.MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, System.IO.SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as CloneDemo;
            }
        }

        public CloneDemo(int p, int q, CloneDemoDeta demoData)
        {
            P = p;
            Q = q;
            DemoData = demoData;
        }

        public CloneDemo ShallowClone()
        {
            return this.Clone() as CloneDemo;
        }

        public override bool Equals(object obj)
        {
            return obj is CloneDemo demo &&
                   P == demo.P &&
                   Q == demo.Q &&
                   EqualityComparer<CloneDemoDeta>.Default.Equals(DemoData, demo.DemoData);
        }

        public override int GetHashCode()
        {
            int hashCode = -1824802191;
            hashCode = hashCode * -1521134295 + P.GetHashCode();
            hashCode = hashCode * -1521134295 + Q.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<CloneDemoDeta>.Default.GetHashCode(DemoData);
            return hashCode;
        }
    }
}