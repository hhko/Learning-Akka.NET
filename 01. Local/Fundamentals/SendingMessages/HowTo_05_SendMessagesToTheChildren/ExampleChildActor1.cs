using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_05_SendMessagesToTheChildren
{
    public class ExampleChildActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleChildActor1());
        }

        public ExampleChildActor1()
        {
            Receive<string>(value => Handle(value));
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleChildActor1",
                    value));
        }
    }
}
