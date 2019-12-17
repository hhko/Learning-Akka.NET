using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_04_UnderstandActorPath
{
    public class ExampleRootActor1 : ReceiveActor
    {
        private readonly IActorRef _childActorRef;

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            //
            // Self.Path
            //

            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleRootActor1 Constructor",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path));        // ActorPath 클래스 리턴

            _childActorRef = Context.ActorOf(
                ExampleChildActor1.Props(),
                "child-actor-name1");
        }
    }
}
