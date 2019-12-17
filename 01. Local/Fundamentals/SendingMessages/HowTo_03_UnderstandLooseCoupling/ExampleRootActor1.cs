using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_03_UnderstandLooseCoupling
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : ...ing",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 Constructor"));

            Thread.Sleep(2000);
            Receive<string>(value => Handle(value));

            Console.WriteLine(
                string.Format("{0:000} {1,-40} : ...ed",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 Constructor"));
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : ...ing",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 Handle"));

            Thread.Sleep(2000);

            Console.WriteLine(
                string.Format("{0:000} {1,-40} : ...ed",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 Handle"));
        }
    }
}
