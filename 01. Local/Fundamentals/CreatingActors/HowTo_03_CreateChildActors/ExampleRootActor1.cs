using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_03_CreateChildActors
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
                string.Format("{0:000} : ExampleRootActor1 Constructor",
                    Thread.CurrentThread.ManagedThreadId));

            //
            // Child Actor 생성: Context.ActorOf, Props.Create, IActorRef
            //
            IActorRef childActor = Context.ActorOf(
                ExampleChildActor1.Props(), 
                "child-actor-name1");
        }
    }
}
