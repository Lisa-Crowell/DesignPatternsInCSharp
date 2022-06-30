using System;
using System.Collections.Generic;
using static System.Console;

// Open-Closed Principle - a class/system etc. should be open to extension through inheritance,
// but closed to modification
// in this example we extend the BetterFilter class by adding additional classes that make use of
// the BetterFilter class, but do not modify the BetterFilter class itself
namespace Open-ClosePrinciple
{
  public enum Color
  {
    Red, Green, Blue
  }

  public enum Size
  {
    Small, Medium, Large, X_Large
  }

  public class Product
  {
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {
      Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
      Color = color;
      Size = size;
    }
  }

  public class ProductFilter
  {
    // let's restrict the type of filter queries on products
    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
      foreach (var p in products)
        if (p.Color == color)
          yield return p;
    }
    
    public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
      foreach (var p in products)
        if (p.Size == size)
          yield return p;
    }
    // add ability to filter by size and color (adding this breaks the OCP as we are modifing the filter after
    // it has been written and possibly shipped to client)
    public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
    {
      foreach (var p in products)
        if (p.Size == size && p.Color == color)
          yield return p;
    } 
  }

  // we introduce two new interfaces that are open for extension by introducing an Enterprise Pattern called 
  // the Specification Pattern

  public interface ISpecification<T>
  {
    bool IsSatisfied(Product p);
  }

  public interface IFilter<T>
  {
    // items->feed the interface a bunch of items, spec-> tells it what the specification of those items should be
    // and how to filter them and T -> get a bunch of filtered items back
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
  }

  public class ColorSpecification : ISpecification<Product>
  {
    private Color color;

    public ColorSpecification(Color color)
    {
      this.color = color;
    }

    public bool IsSatisfied(Product p)
    {
      return p.Color == color;
    }
  }

  public class SizeSpecification : ISpecification<Product>
  {
    private Size size;

    public SizeSpecification(Size size)
    {
      this.size = size;
    }

    public bool IsSatisfied(Product p)
    {
      return p.Size == size;
    }
  }

  // combinator
  public class AndSpecification<T> : ISpecification<T>
  {
    private ISpecification<T> first, second;

    public AndSpecification(ISpecification<T> first, ISpecification<T> second)
    {
      this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
      this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
    }

    public bool IsSatisfied(Product p)
    {
      return first.IsSatisfied(p) && second.IsSatisfied(p);
    }
  }

  public class BetterFilter : IFilter<Product>
  {
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
      foreach (var i in items)
        if (spec.IsSatisfied(i))
          yield return i;
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      var lime = new Product("Lime", Color.Green, Size.Small)
      var apple = new Product("Apple", Color.Red, Size.Small);
      var tree = new Product("Tree", Color.Green, Size.Medium);
      var house = new Product("House", Color.Blue, Size.Large);

      Product[] products = {lime, apple, tree, house};

      var pf = new ProductFilter();
      WriteLine("Green products (old):");
      foreach (var p in pf.FilterByColor(products, Color.Green))
        WriteLine($" - {p.Name} is green");

      // ^^ BEFORE

      // vv AFTER
      var bf = new BetterFilter();
      WriteLine("Green products (new):");
      foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
        WriteLine($" - {p.Name} is green");

      WriteLine("Large products");
      foreach (var p in bf.Filter(products, new SizeSpecification(Size.Large)))
        WriteLine($" - {p.Name} is large");

      WriteLine("Large blue items");
      foreach (var p in bf.Filter(products,
        new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large)))
      )
      {
        WriteLine($" - {p.Name} is big and blue");
      }
    }
  }
}
