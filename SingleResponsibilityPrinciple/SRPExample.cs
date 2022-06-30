using System.Diagnostics;

// Single Responsibility Principle - a class should be responsible for only one thing,
// meaning it has only one reason to change.

namespace SingleResponsibilityPrinciple
{
    // stores a couple of journal entries and ways of working with the entries
    public class Journal
    {
        private readonly List<string> _entries = new List<string>();

        private static int _count = 0;

        public int AddEntry(string text)
        {
            _entries.Add($"{++_count}: {text}");
            return _count; // Memento pattern
        }

        public void RemoveEntry(int index)
        {
            _entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries);
        }

        // breaks single responsibility principle the code should be split here
        // below is the Persistence class which handles saving the data - effectively splitting the responsibility of 
        // the classes into multiple classes
        public void Save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename, ToString());
        }

        public void Load(string filename)
        {
      
        }

        public void Load(Uri uri)
        {
      
        }
    }

    // handles the responsibility of persisting objects
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I fed a kitty kat today.");
            j.AddEntry("I watered plants this morning.");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"N:\temp\journal.txt";
            p.SaveToFile(j, filename);
            Process.Start(filename);
        }
    }
}