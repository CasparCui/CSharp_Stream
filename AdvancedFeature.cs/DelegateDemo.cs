namespace AdvancedFeature.cs
{
    public class DelegateEvent
    {
        public void DoSomething1()
        {
            System.Console.WriteLine("DoSomething1");
        }

        public void DoSomething2()
        {
            System.Console.WriteLine("DoSomething2");
        }
    }

    internal class DelegateDemo
    {
        public delegate void DoSomething();

        public static void TryToDoDelegate()
        {
            DelegateEvent ev = new DelegateEvent();
            DoSomething something = new DoSomething(ev.DoSomething1);
            something += ev.DoSomething2;
            something();
        }
    }
}