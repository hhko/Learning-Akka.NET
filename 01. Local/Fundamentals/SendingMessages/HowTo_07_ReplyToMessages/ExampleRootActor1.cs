using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_07_ReplyToMessages
{
    public class ExampleRootActor1 : ReceiveActor
    {
        private IActorRef _rootActor2;

        public static Props Props(IActorRef rootActor2)
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1(rootActor2));
        }

        public ExampleRootActor1(IActorRef rootActor2)
        {
            _rootActor2 = rootActor2;

            Receive<string>(value => Handle(value));

            _rootActor2.Tell(2002);
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1",
                    value));
        }
    }
}
