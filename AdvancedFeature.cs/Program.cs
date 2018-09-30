namespace Caspar.CSharpTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args == null)
            {
                throw new System.ArgumentNullException(nameof(args));
            }

            string s = "123";
            s.Print();
        }
    }
}