using System;

//  Prefer small interfaces over large interfaces with lots of functionality; split the large interface into seperate 
// interfaces  YAGNI- you ain't gonna need it
namespace InterfaceSegregationPrinciple;

    public class Document
    {
    }

    public interface IMachine
    {
        void Print(Document d);
        void Fax(Document d);
        void Scan(Document d);
    }

    // ok if you need a multifunction machine
    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Fax(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            // implement print, but old fashioned printer doesn't do anything other than print...
            // so you don't want to pay for what you don't need i.e. Fax and Scan
        }

        public void Fax(Document d)
        {
            throw new System.NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IPrinter
    {
        public void Print(Document d)
        {
            
        }
    }

    public interface IScanner
    {
        public void Scan(Document d)
        {
            
        }
    }

    public class Printer : IPrinter
    {
        public void Print(Document d)
        {
          
        }
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new System.NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner 
    {
        // you can delegate to printer and scanner respectively using the Decorator Pattern (discussed later)
    }

    public struct MultiFunctionMachine : IMultiFunctionDevice
    {
        // compose this out of several modules
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            if (printer == null)
            {
                throw new ArgumentNullException(paramName: nameof(printer));
            }
            if (scanner == null)
            {
                throw new ArgumentNullException(paramName: nameof(scanner));
            }
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d); // Example of Decorator Pattern
        }
    }