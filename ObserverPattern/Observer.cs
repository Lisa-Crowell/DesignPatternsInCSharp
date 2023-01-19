using System;

namespace ObserverPattern // using the 'even' keyword
{
// Observer - an observer is an object that wishes to be informed about events
// happening in the system. The entity generating the events is an observable.
    public class FallsIllEventArgs
    {
        public string Address;
    }

    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this,
                new FallsIllEventArgs { Address = "123 London Road" });
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;
    }

    public class Demo
    {
        static void Main()
        {
            var person = new Person();

            person.FallsIll += CallDoctor;

            person.CatchACold();
        }

        private static void CallDoctor(object sender, FallsIllEventArgs eventArgs)
        {
            Console.WriteLine($"A doctor has been called to {eventArgs.Address}");
        }
    }
}