using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
 // State - a pattern in which the object's behavior is determined by its state.
 // An object transitions from one state to another (something needs to trigger a
 // transition). A formalized construct which manages state and transitions is
 // called a state machine.

 public class Switch
    {
        public State State = new OffState();
        public void On()  { State.On(this);  }
        public void Off() { State.Off(this); }
    }

    public abstract class State
    {
        public virtual void On(Switch sw)
        {
            Console.WriteLine("Light is already on.");
        }

        public virtual void Off(Switch sw)
        {
            Console.WriteLine("Light is already off.");
        }
    }

    public class OnState : State
    {
        public OnState()
        {
            Console.WriteLine("Light turned on.");
        }
        public override void Off(Switch sw)
        {
            Console.WriteLine("Turning light off...");
            sw.State = new OffState();
        }
    }

    public class OffState : State
    {
        public OffState()
        {
            Console.WriteLine("Light turned off.");
        }

        public override void On(Switch sw)
        {
            Console.WriteLine("Turning light on...");
            sw.State = new OnState();
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            // ???
        }
    }
}