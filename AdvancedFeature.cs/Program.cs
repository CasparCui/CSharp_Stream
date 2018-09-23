namespace Caspar.CSharpTest
{
    class Program
    {
        static void Main(string[] args)
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