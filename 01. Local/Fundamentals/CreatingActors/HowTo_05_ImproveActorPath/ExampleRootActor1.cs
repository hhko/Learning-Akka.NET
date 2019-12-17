using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_05_ImproveActorPath
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
            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleRootActor1 Constructor",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path));

            _childActorRef = Context.ActorOf(ExampleChildActor1.Props(), ActorPaths.ChildActor1.Name);
        }
    }
}
