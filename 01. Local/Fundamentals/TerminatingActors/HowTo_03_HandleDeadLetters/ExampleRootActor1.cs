using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_03_HandleDeadLetters
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props(IActorRef rootActor2)
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1(rootActor2));
        }

        public ExampleRootActor1(IActorRef rootActor2)
        {
            rootActor2.Tell("1");
            rootActor2.Tell("2");

            Thread.Sleep(100);
            Context.Stop(rootActor2);
        }

        protected override void PostStop()
        {
            base.PostStop();

            Console.WriteLine(
                string.Format("{0:000} {1,-40}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 PostStop"));
        }
    }
}
