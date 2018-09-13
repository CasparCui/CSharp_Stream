using System;

namespace AdvancedFeature.cs
{
    internal class FormatDemo : IFormattable
    {
        public readonly int p = 1;
        public readonly int b = 2;

        
        #region IFromattable member

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("{0} and {1}", p, b);
        }
        public override string ToString()
        {
            return string.Format("{0} and {1}", p, b);
        }
        #endregion IFromattable member
    }
    internal class FormatDemoFormater : IFormatProvider,ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            FormatDemo demo = arg as FormatDemo;
            if (demo == null)
            {
                return string.Empty;
            }
            switch (format)
            {
                case "+":
                case "Add":
                    return string.Format("{0} + {1}", demo.p, demo.b);

                case "-":
                    return string.Format("{0} - {1}", demo.p, demo.b);

                default:
                    return string.Format("p = {0}, b = {1}", demo.p, demo.b);
            }
        }

        public object GetFormat(Type formatType)
        {
            return formatType.Equals(typeof(ICustomFormatter)) ? (this) : null;
        }

    }
}