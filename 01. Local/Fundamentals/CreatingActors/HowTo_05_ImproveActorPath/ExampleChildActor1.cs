using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_05_ImproveActorPath
{
    public class ExampleChildActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleChildActor1());
        }

        public ExampleChildActor1()
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleChildActor1 Constructor",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path));

            //
            // ExampleChildActor1에서 ExampleRootActor2의 IActorRef 얻기: ActorSelection, ResolveOne
            //

            IActorRef rootActor2 = Context
                //
                // 개선 전
                //.ActorSelection("akka://actorsystem-name/user/root-actor-name2")
                //
                // 개전 후
                .ActorSelection(ActorPaths.RootActor2.Path)
                .ResolveOne(TimeSpan.Zero)
                .Result;

            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleChildActor1에서 ExampleRootActor2 찾기\n  -> {2,-60}",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path,
                    rootActor2.Path));
        }
    }
}
