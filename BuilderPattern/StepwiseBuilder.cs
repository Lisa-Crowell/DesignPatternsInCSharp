namespace BuilderPattern;

/// <summary>
/// The Stepwise Builder Pattern is used to build a complex object step by step.
/// This example uses the Inferface Segregation Principle.
/// </summary>

public enum CarType
{
    Sedan,
    CrossOver
}

public class Car
{
    public CarType Type;
    public int WheelSize;
}

public interface ISpecifyCarType
{
    ISpecifyCarType OfType(CarType type);
}
public interface ISpecifyWheelSize
{
    IBuildCar WithWheels(int size);
}
public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{
    private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
    {
        private Car car = new Car();
        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type;
            return this;
        }
        public IBuildCar WithWheels(int size)
            switch (car.Type)
            {
                case CarType.Crossover when size < 17 || size > 21:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheels for {car.Type}");
            }
            car.WheelSize = size;
            return this;
        }
        public Car Build()
        {
            return car;
        }
    }
    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}

public class Demo
{
    public static void Main()
    {
        var car = CarBuilder.Create()   // ISpecifyCarType
            .OfType(CarType.Crossover)  // ISpecifyWheelSize
            .WithWheels(17)             // IBuildCar
            .Build();
    }
}