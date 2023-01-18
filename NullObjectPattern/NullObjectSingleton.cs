using System;

namespace NullObjectPattern
{
    // This example does violate a core principle of development that of least surprise.
    interface ILog
    {
        public void Warn();

        public static ILog Null => NullLog.Instance; // The static object in an interface is the reason why it violates the principle of least surprise.

        private sealed class NullLog : ILog
        {
            private NullLog() {}

            private static Lazy<NullLog> instance =
                new Lazy<NullLog>(() => new NullLog());

            public static ILog Instance => instance.Value;

            public void Warn()
            {
        
            }
        }
    }

    public class BankAccount
    {
        public BankAccount(ILog log = ILog.Null)
        {
      
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILog log = ILog.Null;
        }
    }
}
