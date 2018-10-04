using System;

namespace Caspar.CSharpTest
{
    #region Delegate Demo

    public delegate void DoSomething();

    public static class DelegateEvent
    {
        public static void DoSomething1()
        {
            Console.WriteLine("DoSomething1");
        }

        public static void DoSomething2()
        {
            Console.WriteLine("DoSomething2");
        }
    }

    internal class DelegateDemo
    {
        public static void TryToDoDelegate()
        {
            DoSomething something = new DoSomething(DelegateEvent.DoSomething1);
            something += DelegateEvent.DoSomething2;
            something();
        }
    }

    #endregion Delegate Demo

    #region Event Demo

    public delegate void PublishEventHandler(object sender, EventInfo e);

    public class PublishEvent
    {
        public event PublishEventHandler OnPublish;

        public void Issue(string info)
        {
            if (OnPublish != null)
            {
                Console.WriteLine("Publish Event Issue");
                Publish(new EventInfo(info));
            }
        }

        protected virtual void Publish(EventInfo e)
        {
            if (OnPublish != null)
            {
                this.OnPublish(this, e);
            }
        }
    }

    public class EventInfo
    {
        public string Info { get; set; }

        public void SetInfo(string info)
        {
            Info = info;
        }

        public EventInfo()
        {
        }

        public EventInfo(string info)
        {
            Info = info;
        }
    }

    public static class UserPublishHandler1
    {
        public static void Alert()
        {
            Console.WriteLine("User1 Alert!");
        }

        public static void AlertEvent(object sender, EventInfo info)
        {
            Console.WriteLine($"User1 Alert {info.Info}");
        }
    }

    public static class UserPublishHandler2
    {
        public static void Alert()
        {
            Console.WriteLine("User2 Alert!");
        }

        public static void AlertEvent(object sender, EventInfo info)
        {
            Console.WriteLine($"User2 Alert {info.Info}");
        }
    }

    public static class PubulishEventDelegateDemo
    {
        public static void DoDelegatePulbisherDemo()
        {
            var publisher = new PublishEvent();
            var info = "TestString1";

            if (info == "TestString1")
            {
                publisher.OnPublish += UserPublishHandler1.AlertEvent;
                publisher.Issue("Test1");
            }
            else
            {
                publisher.OnPublish += UserPublishHandler2.AlertEvent;
                publisher.Issue("Test2");
            }
        }
    }

    #endregion Event Demo

    #region Delegate Call Back Demo

    public delegate string CallBackDelegateDemo(string s1, string s2);

    public class CallBackMethod
    {
        public string DoDelegate(string s1, string s2, CallBackDelegateDemo demo)
        {
            return demo(s1, s2);
        }

        public string CallBack1(string s1, string s2)
        {
            return $"{s1}+{s2}";
        }

        public string CallBack2(string s1, string s2)
        {
            return $"{s1}-{s2}";
        }

        public string CallBack3(string s1, string s2)
        {
            return $"{s1}={s2}";
        }
    }

    public static class CallBackDemo
    {
        public static void DoCallBackDemo(string s1, string s2)
        {
            var t = new CallBackMethod();
            var a1 = t.DoDelegate(s1, s2, new CallBackDelegateDemo(t.CallBack1));
            var a2 = t.DoDelegate(s1, s2, new CallBackDelegateDemo(t.CallBack2));
            var a3 = t.DoDelegate(s1, s2, new CallBackDelegateDemo(t.CallBack3));
            Console.WriteLine($"{a1}//{a2}//{a3}");
        }
    }

    #endregion Delegate Call Back Demo
}