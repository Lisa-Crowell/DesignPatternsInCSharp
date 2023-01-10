using System;
using Autofac;
using static System.Console;

// Proxy - a class that functions as an interface to a particular resource.
// That resource may be remote, expensive to construct, or may require logging or some other added functionality.
// Provides additional fubnctionality on an object, but the nature of that functionality doesn't
// have to be intrinsic to the object intself. 

namespace ProxyPattern // example of Protection Proxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            WriteLine("Car being driven");
        }
    }

    public class CarProxy : ICar
    {
        private Car car = new Car();
        private Driver driver;

        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }

        public void Drive()
        {
            if (driver.Age >= 16)
                car.Drive();
            else
            {
                WriteLine("Driver too young");
            }
        }
    }

    public class Driver
    {
        public int Age { get; set; }
    
        public Driver(int age)
        {
            Age = age;
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            ICar car = new CarProxy(new Driver(12)); // 22
            car.Drive();
        }
    }
}