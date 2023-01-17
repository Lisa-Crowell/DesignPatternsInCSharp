// Memento - a token/handle representing the system state. Lets us roll back to the state
// when the token was generated. May or may not directly expose state information. 

using static System.Console;

namespace MementoPattern
{
    public class Memento
    {
        public int Balance { get; }

        public Memento(int balance)
        {
            Balance = balance;
        }
    }

    public class BankAccount
    {
        private int balance;

        public BankAccount(int balance)
        {
            this.balance = balance;
        }

        public Memento Deposit(int amount)
        {
            balance += amount;
            return new Memento(balance);
        }

        public void Restore(Memento m)
        {
            balance = m.Balance;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount(100); // cannot roll back to the initial state as there is no token for it
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            WriteLine(ba);

            // restore to m1
            ba.Restore(m1);
            WriteLine(ba);

            ba.Restore(m2);
            WriteLine(ba);
        }
    }
}