// Composite - a mechanism for treating individual objects and compositions of
// objects in a uniform manner. It is a structural design pattern that lets clients
// treat the individual objects in a uniform manner.

namespace CompositePattern;

public class GraphicObject
{
    public virtual string Name { get; set; } = 'Group';
    public string Color;
    
    private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
    public Lazy<List<GraphicObject>> Children => children.Value;
    
    public void Print(StringBuilder sb, int depth)
    {
        sb.Append(new string('*', depth))
          .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
          .AppendLine(Name);
        
        foreach (var child in Children)
            child.Print(sb, depth + 1);
    }
    {
        Console.WriteLine($"Name: {Name}, Color: {Color}");
        foreach (var child in Children)
        {
            child.Print();
        }
    }
    public override string ToString()
    {
        var sb = new StringBuilder();
        Print(sb, 0);
        return sb.ToString();
    }
}
public class Circle : GraphicObject
{
    public override string Name => 'Circle';
}
public class Square : GraphicObject
{
    public override string Name => 'Square';
}
static class Program
{
    static void Main(string[] args)
    {
        var drawing = new GraphicObject {Name = "My Drawing"};
        drawing.Children.Add(new Square {Color = "Red"});
        drawing.Children.Add(new Circle {Color = "Yellow"});
        
        var group = new GraphicObject();
        group.Children.Add(new Circle {Color = "Blue"});
        group.Children.Add(new Square {Color = "Blue"});
        drawing.Children.Add(group);
        
        Console.WriteLine(drawing);
    }
}