using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_04_UnderstandActorPath
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

            // 만약 Actor을 찾지 못하면 "ActorNotFoundException" 예외가 발생된다.
            IActorRef rootActor2 = Context
                .ActorSelection("akka://actorsystem-name/user/root-actor-name2")
                .ResolveOne(TimeSpan.Zero)      // Task<IActorRef> 리턴
                .Result;                        // IActorRef 리턴

            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleChildActor1에서 ExampleRootActor2 찾기\n  -> {2,-60}",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path,
                    rootActor2.Path));
        }
    }
}
